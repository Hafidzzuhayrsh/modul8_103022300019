// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        BankTransferConfig config = BankTransferConfig.LoadConfig();
        bool isEnglish = config.lang == "en";
        Console.WriteLine(isEnglish ? "Please insert the amount of money to transfer:" :
                                      "Masukkan jumlah uang yang akan di-transfer:");
        int amount = int.Parse(Console.ReadLine());
        int fee = amount <= config.transfer.threshold ? config.transfer.low_fee : config.transfer.high_fee;
        int total = amount + fee;

        Console.WriteLine(isEnglish ? $"Transfer fee = {fee}" : $"Biaya transfer = {fee}");
        Console.WriteLine(isEnglish ? $"Total amount = {total}" : $"Total biaya = {total}");

        Console.WriteLine(isEnglish ? "Select transfer method:" : "Pilih metode transfer:");

        for (int i = 0; i < config.methods.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {config.methods[i]}");
        }
        Console.ReadLine();
        string confirmationPrompt = isEnglish ?
            $"Please type \"{config.confirmation.en}\" to confirm the transaction:" :
            $"Ketik \"{config.confirmation.id}\" untuk mengkonfirmasi transaksi:";
        Console.WriteLine(confirmationPrompt);
        string userConfirm = Console.ReadLine();
        bool confirmed = (isEnglish && userConfirm == config.confirmation.en) ||
                         (!isEnglish && userConfirm == config.confirmation.id);
        if (confirmed)
        {
            Console.WriteLine(isEnglish ? "The transfer is completed" : "Proses transfer berhasil");
        }
        else
        {
            Console.WriteLine(isEnglish ? "Transfer is cancelled" : "Transfer dibatalkan");
        }
    }
}

