// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using dotnetEval.Models;
// using dotnetEval.repository;

// namespace dotnetEval.service
// {

//     public class MembresService
//     {
//         private readonly MembresRepository _membreRepository;
//         private ExeptionMessage _exeptionMessage;
//         public ExcelExporterMembres _excelExporterMembres;

//         public MembresService(MembresRepository membreRepository,ExeptionMessage exception,ExcelExporterMembres excelExporterMembres)
//         {
//             _membreRepository = membreRepository;
//             _exeptionMessage = exception;
//             _excelExporterMembres = excelExporterMembres;
//         }

//         public List<Membres> GetAllMembres()
//         {
//             return _membreRepository.FindAll();
//         }
//         public Membres FindByIdMembres(String id)
//         {
//             return _membreRepository.FindById(id);
//         }
//         public void InsertionMembres(string name,string email,string password,DateTime dateNaissance,string matriculation,string statues)
//         {
//             _membreRepository.InsertionMembres(new Membres(name,email,password,dateNaissance,matriculation,statues));
//         }
//         public void InsertionMembresSpecifique(string name,string email,string password,DateTime dateNaissance,string matriculation,string statues)
//         {

//             try
//             {
//                 name = _exeptionMessage.ValidateName(name);
//                 password = _exeptionMessage.ValidatePassword(password);
//                 dateNaissance = _exeptionMessage.ValidateDateOfBirth(dateNaissance);
//                 _membreRepository.InsertionMembresSpecifique(
//                 name,
//                 email,
//                 password,
//                 dateNaissance,
//                 matriculation,
//                 statues);
//             }
//             catch (ArgumentException ex)
//             {
//                 throw new ArgumentException(ex.Message);
//             }
//         }
//         public void UpdateMembresSpecifique(String id,string name,string email,string password,DateTime dateNaissance,string matriculation,string statues)
//         {
//             try
//             {
//                 name = _exeptionMessage.ValidateName(name);
//                 password = _exeptionMessage.ValidatePassword(password);
//                 dateNaissance = _exeptionMessage.ValidateDateOfBirth(dateNaissance);
//                 _membreRepository.UpdateMembresSpecifique(
//                 id,
//                 name,
//                 email,
//                 password,
//                 dateNaissance,
//                 matriculation,
//                 statues);
//             }
//             catch (ArgumentException ex)
//             {
//                 throw new ArgumentException(ex.Message);
//             }
//         }

//         public void DeleteMembres(string id)
//         {
//             _membreRepository.DeleteMembre(id);
//         }

//         public string GetStatues(string id){
//             return _membreRepository.GetStatues(id);
//         }

//         public void exportExcelMembres(){
//             List<Membres> membres= _membreRepository.FindAll();
//             _excelExporterMembres.ExportToExcel("D:\\Fianarana\\s6\\dotnetEval\\excel\\","membres.ods",membres);
//         }

//         public String VerificationLogin(string email, string password)
//         {
//             try{  
//                 String id=_membreRepository.VerificationLogin(email, password);
//                 return id;
//             }
//             catch (ArgumentException ex){ throw new ArgumentException(ex.Message); }
//         }

//         internal void RegisterBase(string name, string lastName, string email, DateTime dateNaissance,string genre, string password, string passwordConfirm)
//         {
//             try
//             {
//                 name = _exeptionMessage.ValidateName(name);
//                 lastName = _exeptionMessage.ValidateName(lastName);
//                 password = _exeptionMessage.ConfirmationPassword(_exeptionMessage.ValidatePassword(password), passwordConfirm);
//                 dateNaissance = _exeptionMessage.ValidateDateOfBirth(dateNaissance);
//                 _membreRepository.RegisterBase(name, lastName, email, dateNaissance,genre, password);
//             }
//             catch (ArgumentException ex){throw new ArgumentException(ex.Message);}
//         }
//     }
// }