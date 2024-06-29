
// namespace dotnetEval;

// public class ChampService
// {
//     private readonly ChampRepository _champRepository;
//     public ChampService(ChampRepository champRepository){
//         _champRepository = champRepository;
//     }
//     public void AddChamps(Champs champs)
//     {
//         if (champs == null){
//             throw new ArgumentNullException("champs vide");
//         }
//         else{
//             _champRepository.AddChampBase(champs);
//         }
//     }

//     public List<Champs> AllChamps()
//     {
//         try{
//            return _champRepository.AllChamps();
//         }
//         catch (System.Exception){   
//             throw;
//         }
//     }

//     public  Champs FindByIdChamps(int id)
//     {
//         return _champRepository.FindByIdChamps(id);
//     }

// }
