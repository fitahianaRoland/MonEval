using System;
using iText.Kernel.Pdf;
using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using dotnetEval.Models;


namespace dotnetEval.service
{

    public class PdfService 
    {

    private readonly ClassementGeneralViewRepository _classementGeneralViewRepository;
    public PdfService(ClassementGeneralViewRepository classementGeneralViewRepository){
        _classementGeneralViewRepository = classementGeneralViewRepository;
    }
        public void GenerationPdf(string path , List<Classement_general_view> classement_General_View)
        {
            Console.WriteLine("nom:" + classement_General_View[0].Equipe);
            // Création d'un nouveau document PDF
            using (PdfWriter writer = new PdfWriter(path))
            using (PdfDocument pdfDoc = new PdfDocument(writer))
            {
                Document document = new Document(pdfDoc);

                Image leftImage = new Image(ImageDataFactory.Create("D:\\Fianarana\\s6\\dotnetEval\\wwwroot\\img\\medaille.jpg"))
                    .SetHeight(185)
                    .SetWidth(185)
                    .SetFixedPosition(30, 630); // Positionner à gauche

                Image rightImage = new Image(ImageDataFactory.Create("D:\\Fianarana\\s6\\dotnetEval\\wwwroot\\img\\medaille.jpg"))
                    .SetHeight(185)
                    .SetWidth(185)
                    .SetFixedPosition(pdfDoc.GetDefaultPageSize().GetWidth() - 200, 630); // Positionner à droite

                // Calculer la position centrale pour le titre
                float pageWidth = pdfDoc.GetDefaultPageSize().GetWidth();
                float titleWidth = 300; // Largeur estimée du titre
                float titleXPosition = (pageWidth - titleWidth) / 2;

                // Ajouter le titre
                Paragraph titletop = new Paragraph("RUNNING")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica-Bold"))
                    .SetFontSize(40)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFixedPosition(titleXPosition, 700, titleWidth);
                Paragraph titlebottom = new Paragraph("Champion")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica-Bold"))
                    .SetFontSize(40)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFixedPosition(titleXPosition, 670, titleWidth);

                Paragraph subTitle = new Paragraph("EQUIPE " + classement_General_View[0].Equipe)
                    .SetFont(PdfFontFactory.CreateFont("Helvetica"))
                    .SetFontSize(30)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFixedPosition(titleXPosition, 620, titleWidth); 
                Paragraph text = new Paragraph("ceci est un certificat pour le meilleur equipe durant cette annees . ")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica"))
                    .SetFontSize(10)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFixedPosition(titleXPosition, 610, titleWidth); 

                // Ajouter les éléments au document
                document.Add(leftImage);
                document.Add(rightImage);
                document.Add(titletop);
                document.Add(titlebottom);
                document.Add(subTitle);
                document.Add(text);



                // Footer
                float footerYPosition = 580;
                float footerElementSpacing = 120;

                // Signature
                Paragraph signature = new Paragraph("Signature")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica"))
                    .SetFontSize(12)
                    .SetFixedPosition(50, footerYPosition, 100);

                // Date
                Paragraph date = new Paragraph("Date : 10 Juin 2010")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica"))
                    .SetFontSize(12)
                    .SetFixedPosition(50 + footerElementSpacing, footerYPosition, 150);

                // Distance parcourue
                Paragraph distance = new Paragraph("Distance parcourue : " + classement_General_View[0].Longueur +"km")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica"))
                    .SetFontSize(12)
                    .SetFixedPosition(50 + 2 * footerElementSpacing, footerYPosition, 200);

                Paragraph time = new Paragraph("temps 15:30 mn")
                    .SetFont(PdfFontFactory.CreateFont("Helvetica"))
                    .SetFontSize(12)
                    .SetFixedPosition(50 + 2 * footerElementSpacing, footerYPosition - 20 , 250);
                // Ajouter les éléments du footer au document
                document.Add(signature);
                document.Add(date);
                document.Add(distance);
                document.Add(time);


                // Fermer le document
                document.Close();
            }
        }
    }
}
