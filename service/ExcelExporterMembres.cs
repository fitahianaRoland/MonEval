// using dotnetEval.Models;
// using OfficeOpenXml;
// using System;
// using System.Collections.Generic;
// using System.IO;

// public class ExcelExporterMembres
// { 
//     public void ExportToExcel(string path, string nameFile, List<Membres> membres)
//     {
//         // Créer un nouveau fichier Excel
//         var newFile = new FileInfo(Path.Combine(path, nameFile));

//         using (var package = new ExcelPackage(newFile))
//         {
//             // Ajouter une feuille de calcul au classeur
//             ExcelWorksheet worksheet;
//             if (package.Workbook.Worksheets.Count > 0)
//             {
//                 worksheet = package.Workbook.Worksheets["Membres"];
//                 worksheet.Cells.Clear(); // Efface les données existantes si la feuille existe déjà
//             }
//             else
//             {
//                 worksheet = package.Workbook.Worksheets.Add("Membres");
//             }

//             // Ajouter des en-têtes de colonne
//             worksheet.Cells[1, 1].Value = "ID Membre";
//             worksheet.Cells[1, 2].Value = "Nom";
//             worksheet.Cells[1, 3].Value = "Email";
//             worksheet.Cells[1, 4].Value = "Date de Naissance";
//             worksheet.Cells[1, 5].Value = "Matriculation";
//             worksheet.Cells[1, 6].Value = "Statues";

//             // Remplir les données dans les cellules
//             int row = 2;
//             foreach (var membre in membres)
//             {
//                 worksheet.Cells[row, 1].Value = membre.IdMembre;
//                 worksheet.Cells[row, 2].Value = membre.Nom;
//                 worksheet.Cells[row, 3].Value = membre.Email;
//                 worksheet.Cells[row, 4].Value = membre.DateNaissance.ToShortDateString();
//                 worksheet.Cells[row, 5].Value = membre.Matricule;
//                 worksheet.Cells[row, 6].Value = membre.Statues;
//                 row++;
//             }

//             // Auto fit des colonnes
//             worksheet.Cells.AutoFitColumns();

//             // Enregistrer le classeur
//             package.Save();
//         }
//     }
// }
