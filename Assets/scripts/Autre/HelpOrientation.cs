using System;
using UnityEngine;

static class HelpOrientation
{
    private static float tolerance = 0.001f; //j'ai créer une variable de tolérance pour contrebalancer les erreurs d'arrondie du float
    static public Vector3 GetVecteurNormal(Vector3 pointA, Vector3 pointB, Vector3 pointC, float lenghtMax = -1)
    {
        //La distance maximum n'est pas obligatoire, donc si elle est négatif, je ne vérifierais pas cette condition
        bool hasLenghtMax = lenghtMax > 0;
        //pour obtenir un vecteur scalaire a un plan, il suffit le produit scalaire de deux vecteur non collineaire de ce plan.
        //Par conséquent je réalise les opération suivante :
        
        //Création des vecteurs a partir des points données
        Vector3 vecteurNormal = Vector3.zero;
        Vector3 vecteurAB = pointB - pointA;
        Vector3 vecteurAC = pointC - pointA;
        Vector3 vecteurBC = pointC - pointB;
        
        //Tout d'abord une grosse phase vérification pour que la fonction soit correcte
        if (hasLenghtMax)
        {
            //Je vérifie qu'aucun point n'est trop éloigné, dans mon cas ca voudrait dire que l'un des point n'est pas du tout sur le bon objet
            if (vecteurAB.magnitude > lenghtMax)
            {
                throw new ArgumentException("Un des points est trop éloignée des autres de " + vecteurAB.magnitude);
            }
            else if (vecteurAC.magnitude > lenghtMax)
            {
                throw new ArgumentException("Un des points est trop éloignée des autres de " + vecteurAC.magnitude);
            }
            else if (vecteurBC.magnitude > lenghtMax)
            {
                throw new ArgumentException("Un des points est trop éloignée des autres de " + vecteurBC.magnitude);
            }
        }
        
        //Je vérifie qu'il n'y a pas de vecteur Null
        if (Math.Abs(vecteurAB.magnitude) < tolerance ||Math.Abs(vecteurAC.magnitude) < tolerance ||Math.Abs(vecteurBC.magnitude) < tolerance )
        {
            //J'envoie une erreur personnalisé
            throw new SamePointExeption();
        }
        
        //Test pour vérifier que les points ne sont pas Collineaire, condition obligatoire pour former un plan a partir de 3 points
        if (Math.Abs(vecteurAB.x/vecteurAC.x - vecteurAB.y/vecteurAC.y) < tolerance && Math.Abs(vecteurAB.y/vecteurAC.y - vecteurAB.z/vecteurAC.z) < tolerance)
        {
            //J'envoie une erreur personnalisé
            throw new CollinearityExeption(3);
        }
        
        //Une fois la phase de vérification réaliser, je passe au produit scalaire, que j'ai réaliser dans une autre fonction pour l'utiliser à d'autre endroit.

        vecteurNormal = GetScalarProduct(vecteurAB , vecteurAC);
        return vecteurNormal;
    }

    static public Vector3 GetScalarProduct(Vector3 a, Vector3 b)
    {
        //Operation du produit scalaire
        Vector3 result = new Vector3(
            (a.y * b.z) - (a.z * b.y),
            (a.z * b.x) - (a.x * b.z),
            (a.x * b.y) - (a.y * b.x));
        return result;
    }
}

//Création de mes propres Exeption pour gérer des problème directement lié au fait de chercher un vecteur normal a un plan.
public class CollinearityExeption : Exception
{
    public CollinearityExeption(int nombreDePoint) : base(
        ((nombreDePoint < 3)
            ? throw new ArgumentOutOfRangeException("nombreDePoint", "There are less than 3 points.")
            : nombreDePoint.ToString()) 
        + " are collinear.") { }
}
public class SamePointExeption : Exception
{
    public SamePointExeption() : base("At least two points have the same coordinate.") { }
}
