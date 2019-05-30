using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Coffre
{
    private static readonly Dictionary<string, object> coffre = new Dictionary<string, object>();

    public static void Créer()
    {
        //Si jamais on a des choses à ajouter quand on crée notre beau coffre
        //coffre.Remplir("étiquette", "valeur");
        Remplir("mode", "solo");
    }

    public static void Remplir(string étiquette, object objet)
    {
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
