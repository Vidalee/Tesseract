﻿using System.Collections.Generic;
using UnityEngine;

//Fait avec amour pour la langue française par Thomas
//Sponsorisé par #ProjetVoltaire

class Coffre
{
    private static readonly Dictionary<string, object> coffre = new Dictionary<string, object>();

    
    //Si jamais on a des choses à ajouter quand on crée notre beau coffre
    //coffre.Remplir("étiquette", "valeur");
    public static void Créer()
    {
        Remplir("seed", 2);
        Remplir("mode", "solo");
        Remplir("id", "1");
    }

    public static void Remplir(string étiquette, object objet)
    {
        //Debug.Log("remplir: " + étiquette + " | " + objet);
        if (Existe(étiquette))
            Vider(étiquette);
        coffre.Add(étiquette, objet);
    }

    public static void Vider(string étiquette)
    {
        coffre.Remove(étiquette);
    }

    public static object Regarder(string étiquette)
    {
        if (Existe(étiquette))
            return coffre[étiquette];
        return null;
    }

    public static bool Existe(string étiquette)
    {
        return coffre.ContainsKey(étiquette);
    }
}
