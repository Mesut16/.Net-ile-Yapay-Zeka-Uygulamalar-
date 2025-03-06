using Tesseract;
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Karakter okuması yapılacak resim yolu: ");
        string imagepath = Console.ReadLine();
        Console.WriteLine();

        string testDataPath = @"C:\tessdata";

        try
        {
            using (var engine = new TesseractEngine(testDataPath, "tur", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagepath))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        Console.WriteLine("Resimden okunan metin: ");
                        Console.WriteLine(text);
                    }
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }
    }
}