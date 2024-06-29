using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dotnetEval.service
{
    public class ExeptionMessage
    {
    public String ValidateName(string name)
    {
        // Vérifier la taille du nom
        if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentException("Le nom ne peut pas être vide !!");}
        if (name.Length > 50){  throw new ArgumentException("nom trop long !!"); }
        return name.TrimStart();;
    }

    public String ValidatePassword(string password)
    {
        // Vérifier la longueur minimale
        if (password.Length < 4){throw new ArgumentException("mot de passe doit etre superieur a 3 !!");;}
        // Vérifier la présence de caractères spéciaux, chiffres, lettres majuscules et minuscules
        // if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$")) { throw new ArgumentException("doit contenir des caractere speciaux"); }

        // Vérifier l'absence de mots de passe couramment utilisés (à adapter selon vos besoins)
        string[] commonPasswords = { "password", "qwerty", "abc123" };
        if (commonPasswords.Contains(password)){ throw new ArgumentException("mot de passe trop utilise !!"); }

        return password;
    }

    public DateTime ValidateDateOfBirth(DateTime dateOfBirth)
    {
        // Vérifier si la date de naissance est dans le passé
        if (dateOfBirth > DateTime.Today){throw new ArgumentException("date invalide !!");}
        DateTime minDate = new DateTime(1800, 1, 1);
        if (dateOfBirth < minDate){ throw new ArgumentException("La date de naissance doit être après le 01/01/1800.");}
        return dateOfBirth;
    }
    public String ConfirmationPassword(String password,String confirmationPassword)
    {
        // Vérifier si la date de naissance est dans le passé
        if (password!=confirmationPassword){throw new ArgumentException("les deux mot de passe doit etre identique !!");}
        return confirmationPassword;
    }

    public void ConfirmationHeure(DateTime heure_debut,DateTime heure_arrive)
    {
        if (heure_debut >= heure_arrive){throw new ArgumentException("heure debut doit etre inferieur a heure fin");}
    }

    public string ValidateNumero(string numero)
    {
        if (numero.Length != 10){
            throw new ArgumentException("Le numéro doit contenir exactement 5 caractères.");
        }
        if (!Regex.IsMatch(numero, @"^\d+$")){
        throw new ArgumentException("Le numéro doit contenir uniquement des chiffres.");
        }
        // Vérifier si le numéro commence par l'un des préfixes autorisés
        string[] allowedPrefixes = { "034", "033", "032", "038" };
        bool isValidPrefix = false;
        foreach (string prefix in allowedPrefixes){
            if (numero.StartsWith(prefix)){
                isValidPrefix = true;
                break;
            }
        }

        if (!isValidPrefix)
        {
            throw new ArgumentException("Le numéro doit commencer par 034, 033, 032 ou 038.");
        }
        return numero.Substring(1);
    }

    }
}