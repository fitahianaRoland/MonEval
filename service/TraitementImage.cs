using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

public class TraitementImage
{
    public string CompressImage(IFormFile image, string destinationDirectory, long maxSizeBytes, int quality)
    {
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
        string destinationImagePath = Path.Combine(destinationDirectory, uniqueFileName);
        using (var stream = new MemoryStream())
        {
            // Copier le contenu de l'image téléchargée dans un MemoryStream
            image.CopyTo(stream);

            // Charger l'image à partir du MemoryStream
            using (var img = Image.Load(stream.ToArray()))
            {
                // Calculer la taille actuelle de l'image
                long currentSizeBytes = stream.Length;

                // Si la taille de l'image dépasse la limite spécifiée, compresser davantage
                if (currentSizeBytes > maxSizeBytes)
                {
                    // Calculer le facteur de compression nécessaire pour atteindre la taille cible
                    double compressionFactor = (double)maxSizeBytes / currentSizeBytes;

                    // Redimensionner la qualité de l'image en fonction du facteur de compression
                    var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder
                    {
                        Quality = (int)Math.Round(quality * compressionFactor) // Réduire la qualité en fonction du facteur de compression
                    };

                    // Sauvegarder l'image compressée dans un fichier sur le disque
                    img.Save(destinationImagePath, encoder);
                }
                else
                {
                    // Si la taille de l'image est déjà inférieure à la limite, pas besoin de compresser
                    // Sauvegarder l'image d'origine dans un fichier sur le disque
                    File.WriteAllBytes(destinationImagePath, stream.ToArray());
                }
            }
        }
        return uniqueFileName;
    }

    public string DoCompressImage(IFormFile image,String path)
    {
        string NewnomImage ="";
        // Vérifiez si une image a été téléchargée
        try
        {
            if (image != null && image.Length > 0)
            {
                long maxSizeBytes = 500 * 1024; // Limite de taille en octets (500 ko)
                int quality = 75; // Qualité de compression initiale
                string destinationImagePath = path; // Chemin de destination pour l'image compressée
    
                // Compressez l'image et enregistrez-la dans un fichier sur le disque
                NewnomImage = CompressImage(image, destinationImagePath, maxSizeBytes, quality);
            }
            else{ Console.WriteLine("Aucune Image Telecharger"); }
            return NewnomImage;
        }
        catch (ArgumentException){   throw new ArgumentException("Aucune image téléchargée !");}
    }
}

