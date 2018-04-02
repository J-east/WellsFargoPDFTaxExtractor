using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WellsFargoPDFTaxExtractor {
    public static class PDFExtractor {
        public static void ExtractBusinessData(string directory) {
            PdfReader pdfReader;

            try {
                foreach (string f in Directory.GetFiles(directory, "*.pdf")) {
                    pdfReader = new PdfReader(f);

                    for (int page = 1; page <= pdfReader.NumberOfPages; page++) {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        // we've got something
                        if (currentText.Contains("Date Number Description Credits Debits balance")) {
                            long accountNumber = 0;
                            bool onTransaction = false;
                            bool readyToInsert;
                            bool hasDate = false;
                            DateTime dateOfTransaction = DateTime.MinValue;
                            string typeOfTrans = "";
                            string desc = "";
                            double amount = 0;

                            foreach (string l in currentText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)) {
                                // I want to know it
                                if (l.Contains("Account number: ") && accountNumber == 0) {
                                    long.TryParse(l.Split(' ')[2], out accountNumber);
                                }
                                // we're looking for a line with #/# pattern, because regex is slow, linq would be better, let's see what can be done...
                                // get the index of all the numbers
                                char[] characters = l.ToCharArray();
                                int[] v = characters.Select((b, i) => (b >= '0' && b <= '9') ? i : -1).Where(i => i != -1).ToArray();
                                bool hasPattern = false;
                                foreach (int i in v) {
                                    if (i > characters.Length - 3) {
                                        hasPattern = false;
                                        break;
                                    }
                                    if (characters[i + 1] == '/' && characters[i + 2] >= '0' && characters[i + 2] <= '9') {
                                        hasPattern = true;
                                        break;
                                    }
                                }
                                if (!hasPattern && !onTransaction) {
                                    continue;
                                }
                                else if (hasPattern && onTransaction) {  // transaction completed, new transaction
                                    if (amount == 0) {  // we have some problems
                                        Console.WriteLine("issue, look into it");
                                    }
                                    else {
                                        DataAccess.CheckAndInsertTransaction(accountNumber, dateOfTransaction, typeOfTrans, desc.Length == 0 ? l : desc, amount);
                                    }
                                    desc = "";
                                    hasDate = false;
                                    typeOfTrans = "";
                                    amount = 0;
                                    readyToInsert = false;
                                }

                                if (l.Contains("Beginning balance on") || l.Contains("Ending balance on")) {
                                    continue;
                                }

                                if (!hasDate) {
                                    DateTime.TryParse(l.Split(' ').First(), out dateOfTransaction);
                                    hasDate = dateOfTransaction > DateTime.MinValue;
                                }

                                if (l.Contains("Business to Business ACH Debit")) {
                                    typeOfTrans = "B2B ACH Debit";
                                    desc = l.Substring(l.IndexOf("Business to Business ACH Debit") + "Business to Business ACH Debit".Length);
                                    onTransaction = true;
                                }
                                else if (l.Contains("Deposited OR Cashed Check")) {
                                    typeOfTrans = "deposited check";
                                    onTransaction = true;
                                }
                                else if (l.Contains("Online Transfer")) {
                                    typeOfTrans = "online transfer";
                                    desc = l.Substring(l.IndexOf("online transfer") + "online transfer".Length);
                                    onTransaction = true;
                                }
                                else if (l.Contains("Purchase")) {
                                    typeOfTrans = "Purchase";
                                    desc = l.Substring(l.IndexOf("Business to Business ACH Debit") + "Business to Business ACH Debit".Length);
                                    onTransaction = true;
                                }

                                // the actual value always comes back as something like this: 750.00 778.49
                                // it might be on the same line as other stuff, but it is always at the end
                                // this first number of the two is what matters, or in the case of a deposit, there will only be one number
                                if (Regex.Matches(l, @"(\D)\s*([.\d,]+)").Cast<Match>().Any(m => m.Value.Contains('.'))) {  // make sure it's got the decimal
                                    try {
                                        if (typeOfTrans == "online transfer") {
                                            amount = double.Parse(l.Split(' ').Last());
                                        }
                                        else {
                                            amount = double.Parse(l.Split(' ')[l.Split(' ').Length - 2]);
                                        }

                                        readyToInsert = true;
                                    }
                                    catch { desc += l; }   // just means there was a dollar like pattern in the description
                                }
                                else {  // add to the description
                                    desc += l;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                e.ToString();
            }
        }

        public static void ExtractCheckingData(string directory) {
            PdfReader pdfReader;

            try {
                foreach (string f in Directory.GetFiles(directory, "*.pdf")) {
                    pdfReader = new PdfReader(f);

                    for (int page = 1; page <= pdfReader.NumberOfPages; page++) {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        // we've got something
                        if (currentText.Contains("Date Number Description Additions Subtractions balance")) {
                            long accountNumber = 0;
                            bool onTransaction = false;
                            bool hasDate = false;
                            DateTime dateOfTransaction = DateTime.MinValue;
                            string typeOfTrans = "";
                            string desc = "";
                            double amount = 0;
                            bool startOfTransactions = false;
                            bool addedDesc;

                            var textArray = currentText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                            int lengthOfArr = textArray.Length;
                            // the date format is ALWAYS in the lead position, so that is really the best delimiter for breaking up transactions
                            // the beginning is 'Date Number Description Additions Subtractions balance'
                            // the end is 'Ending balance on ...'
                            for (int index = 0; index < lengthOfArr; index++) {
                                addedDesc = false;
                                string l = textArray[index];

                                if (l == "Date Number Description Additions Subtractions balance") {
                                    startOfTransactions = true;
                                    continue;
                                }

                                if (l.Contains("Account number: ") && accountNumber == 0) {
                                    long.TryParse(l.Split(' ')[2], out accountNumber);
                                }

                                if (startOfTransactions) {
                                    // we're looking for a line with #/# pattern, specifically in the lead position
                                    // get the index of all the numbers
                                    char[] characters = l.ToCharArray();
                                    bool hasPattern = false;
                                    if (l.Length > 5
                                        &&
                                        (l[0] >= '0' && l[0] <= '9'
                                        && l[1] >= '0' && l[1] <= '9'
                                        && l[2] == '/'
                                        && l[3] >= '0' && l[3] <= '9')
                                        ||
                                        (l[0] >= '0' && l[0] <= '9'
                                        && l[1] == '/'
                                        && l[2] >= '0' && l[2] <= '9')
                                        ) {
                                        hasPattern = true;
                                    }

                                    if (!hasPattern && !onTransaction) {
                                        continue;
                                    }
                                    else if ((l.Contains("Ending Balance on") || hasPattern) && onTransaction) {  // transaction completed, new transaction
                                        if (amount == 0) {  // we have some problems
                                            Console.WriteLine("issue, look into it");
                                        }
                                        else {
                                            DataAccess.CheckAndInsertTransaction(accountNumber, dateOfTransaction, typeOfTrans, desc.Length == 0 ? l : desc, amount);
                                        }
                                        desc = "";
                                        hasDate = false;
                                        typeOfTrans = "";
                                        amount = 0;
                                    }

                                    if (l.Contains("Beginning balance on")) {
                                        continue;
                                    }
                                    
                                    if (l.Contains("Ending balance on") && startOfTransactions) {
                                        startOfTransactions = false;    // we're done for now
                                    }

                                    if (!hasDate) {
                                        DateTime.TryParse(l.Split(' ').First(), out dateOfTransaction);
                                        hasDate = dateOfTransaction > DateTime.MinValue;
                                    }

                                    if (l.Contains("Business to Business ACH Debit")) {
                                        typeOfTrans = "B2B ACH Debit";
                                        desc = l.Substring(l.IndexOf("Business to Business ACH Debit") + "Business to Business ACH Debit".Length);
                                        addedDesc = true;
                                        onTransaction = true;
                                    }
                                    else if (l.Contains("ATM Withdrawal")) {
                                        typeOfTrans = "ATM Withdrawl";
                                        onTransaction = true;
                                    }
                                    else if (l.Contains("Deposited OR Cashed Check")) {
                                        typeOfTrans = "deposited check";
                                        onTransaction = true;
                                    }
                                    else if (l.Contains("Online Transfer")) {
                                        typeOfTrans = "online transfer";
                                        desc = l.Split(' ').Skip(1).Aggregate((a, b) => a + ' ' + b);
                                        addedDesc = true;
                                        onTransaction = true;
                                    }
                                    else if (l.Contains("Purchase")) {
                                        typeOfTrans = "Purchase";
                                        desc = l.Substring(l.IndexOf("Business to Business ACH Debit") + "Business to Business ACH Debit".Length);
                                        addedDesc = true;
                                        onTransaction = true;
                                    }

                                    // the actual value always comes back as something like this: 750.00 778.49
                                    // it might be on the same line as other stuff, but it is always at the end
                                    // this first number of the two is what matters, or in the case of a deposit, there will only be one number
                                    if (Regex.Matches(l, @"(\D)\s*([.\d,]+)").Cast<Match>().Any(m => m.Value.Contains('.'))) {  // make sure it's got the decimal
                                        // we need to know if there are two numbers, because if that is the case, we only want the first one
                                        if (Regex.Matches(l, @"(\D)\s*([.\d,]+)").Cast<Match>().Count(m => m.Value.Contains('.')) > 1) {
                                            try {
                                                amount = double.Parse(l.Split(' ')[l.Split(' ').Length - 2]);
                                            }
                                            catch {
                                                desc += l;
                                            }
                                        }
                                        else {
                                            try {
                                                if (typeOfTrans == "online transfer") {
                                                    amount = double.Parse(l.Split(' ').Last());
                                                }
                                                else {
                                                    amount = double.Parse(l.Split(' ')[l.Split(' ').Length - 1]);
                                                }
                                            }
                                            catch { desc += l; }   // just means there was a dollar like pattern in the description
                                        }

                                    }
                                    else if (!addedDesc) {
                                        desc += l;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                e.ToString();
            }
        }

        private enum typeOfTransaction {
            Payments,
            OtherCredits,
            Purchases
        }
        public static void ExtractCreditData(string directory) {
            PdfReader pdfReader;
            string line = "";

            try {
                foreach (string f in Directory.GetFiles(directory, "*.pdf")) {
                    pdfReader = new PdfReader(f);

                    // with wellsfargo credit statements we have to capture the different types of transactions separately
                    // we've got: 'Payments', 'Other Credits' (ie returns), and 'Purchases, Balance Transfers & Other Charges'
                    // they will come in, in that order
                    typeOfTransaction currentTransaction = typeOfTransaction.Payments;
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++) {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                        // we've got something, next line should be: Payments(if any)
                        if (currentText.Contains("Trans Post Reference Number Description Credits Charges")) {
                            long accountNumber = 0;
                            bool onTransaction = false;
                            bool readyToInsert;
                            bool hasDate = false;
                            DateTime dateOfTransaction = DateTime.MinValue;
                            string typeOfTrans = "";
                            string desc = "";
                            double amount = 0;

                            foreach (string l in currentText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)) {
                                line = l;
                                // I want to know it
                                if (l.Contains("Account Number Ending in") && accountNumber == 0) {
                                    long.TryParse(l.Split(' ')[4], out accountNumber);
                                }

                                if (l == "Payments") {
                                    currentTransaction = typeOfTransaction.Payments;
                                }
                                else if (l == "Other Credits") {
                                    currentTransaction = typeOfTransaction.OtherCredits;
                                }
                                else if (l == "Purchases, Balance Transfers & Other Charges") {
                                    currentTransaction = typeOfTransaction.Purchases;
                                }

                                // we're looking for a line with #/# #/# pattern, because regex is slow, linq would be better, let's see what can be done...
                                // get the index of all the numbers then confirm that they are in the right order
                                // ##/## ##/##

                                if (l.Length > 11
                                    && l[0] >= '0' && l[0] <= '9'
                                    && l[1] >= '0' && l[1] <= '9'
                                    && l[2] == '/'
                                    && l[3] >= '0' && l[3] <= '9'
                                    && l[4] >= '0' && l[4] <= '9'
                                    && l[5] == ' '
                                    && l[6] >= '0' && l[6] <= '9'
                                    && l[7] >= '0' && l[7] <= '9'
                                    && l[8] == '/'
                                    && l[9] >= '0' && l[9] <= '9'
                                    && l[10] >= '0' && l[10] <= '9'
                                    ) {
                                    onTransaction = true;
                                }

                                if (l.Contains("Beginning balance on") || l.Contains("Ending balance on")) {
                                    continue;
                                }

                                if (onTransaction) {
                                    if (dateOfTransaction == DateTime.MinValue) {
                                        DateTime.TryParse(l.Split(' ').First(), out dateOfTransaction);
                                        hasDate = dateOfTransaction > DateTime.MinValue;
                                    }

                                    int lengthOfLine = l.Split(' ').Length;
                                    try {
                                        desc = l.Split(' ').Skip(2).Take(lengthOfLine - 3).Aggregate((a, b) => (a + ' ' + b));
                                    }
                                    catch { // this means there was an issue with the parser breaking the transaction up for some reason
                                        desc = l;
                                        continue;   // still on transaction
                                    }

                                    // the last numbers are always the numbers we want, easy
                                    amount = 0;
                                    try {
                                        if (currentTransaction == typeOfTransaction.Payments || currentTransaction == typeOfTransaction.OtherCredits) {
                                            amount = double.Parse(l.Split(' ')[lengthOfLine - 1]);
                                        }
                                        else {
                                            amount = double.Parse(l.Split(' ')[lengthOfLine - 1]);
                                        }
                                    }
                                    catch {
                                        desc += l;
                                        continue;   // basically we're just adding to the transaction until we get a valid amount
                                    }

                                    bool insertSuccessful = DataAccess.CheckAndInsertTransaction(accountNumber, dateOfTransaction, currentTransaction.ToString(), desc.Length == 0 ? l : desc, amount);
                                    onTransaction = false;
                                    dateOfTransaction = DateTime.MinValue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                e.ToString();
            }
        }
    }
}
