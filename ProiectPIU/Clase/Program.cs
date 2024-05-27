using Clase;
using System;
using System.Collections;
using System.Collections.Generic;
using static Clase.Enumerari;
//TEMA LABORATOR 2
public class Medicament
{
    // Properties
    public int Id { get; set; }
    public string Nume { get; set; }
    public decimal Pret { get; set; }
    public int StocDisponibil { get; set; }
    public CategorieMedicament[] Categorie { get; set; }
    public CaracteristiciMedicament Caracteristici { get; set; }

    // Constants
    private const char SEPARATOR_PRINCIPAL_FISIER = '$';
    private const char SEPARATOR_SECUNDAR_FISIER = ';';
    private const bool SUCCES = true;

    // Constructor
    public Medicament(int id, string nume, decimal pret, int stocDisponibil, CategorieMedicament[] categorie, CaracteristiciMedicament caracteristici)
    {
        Id = id;
        Nume = nume;
        Pret = pret;
        StocDisponibil = stocDisponibil;
        Categorie = categorie;
        Caracteristici = caracteristici;
    }

    // File conversion method
    public string ConversieLaSir_PentruFisier()
    {
        string categoriiString = string.Join(";", Array.ConvertAll(Categorie, x => ((int)x).ToString()));

        return string.Join(SEPARATOR_PRINCIPAL_FISIER.ToString(), Id, Nume ?? "NECUNOSCUT", Pret, StocDisponibil, categoriiString, (int)Caracteristici);
    }

    // Constructor for file reading
    public Medicament(string linieFisier)
    {
        string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);
        Id = Convert.ToInt32(dateFisier[0]);
        Nume = dateFisier[1];
        Pret = Convert.ToDecimal(dateFisier[2]);
        StocDisponibil = Convert.ToInt32(dateFisier[3]);
        string[] categorieTokens = dateFisier[4].Split(SEPARATOR_SECUNDAR_FISIER);
        Categorie = Array.ConvertAll(categorieTokens, x => (CategorieMedicament)Convert.ToInt32(x));
        Caracteristici = (CaracteristiciMedicament)Convert.ToInt32(dateFisier[5]);
    }

    // Copy method
    public void CopyMedicament(Medicament medicament)
    {
        Id = medicament.Id;
        Nume = medicament.Nume;
        Pret = medicament.Pret;
        StocDisponibil = medicament.StocDisponibil;
        Categorie = medicament.Categorie;
        Caracteristici = medicament.Caracteristici;
    }

    // Empty constructor
    public Medicament()
    {
        Id = 0;
        Nume = string.Empty;
        Pret = 0;
        StocDisponibil = 0;
        Categorie = new CategorieMedicament[0]; // Initialize with an empty array
        Caracteristici = 0;
    }

    // Method to display details
    public void AfiseazaDetalii()
    {
        string categoriiString = string.Join(", ", Array.ConvertAll(Categorie, x => x.ToString()));
        Console.WriteLine($"ID: {Id}, Nume: {Nume}, Pret: {Pret}, Stoc disponibil: {StocDisponibil}, Categorie: {categoriiString}, Varsta: {Caracteristici}");
    }
}


public class Vanzare
{
    //TEMA LABORATOR 3 si 4 (Proprietati auto-implemented pentru a doua entitate)
    public int IdVanzare { get; set; }
    public int IdMedicament { get; set; }
    public int Cantitate { get; set; }
    public DateTime Data { get; set; }

    public Vanzare(int idVanzare, int idMedicament, int cantitate, DateTime data)
    {
        IdVanzare = idVanzare;
        IdMedicament = idMedicament;
        Cantitate = cantitate;
        Data = data;
    }

    public void AfiseazaDetalii()
    {
        Console.WriteLine($"ID Vanzare: {IdVanzare}, ID Medicament: {IdMedicament}, Cantitate: {Cantitate}, Data: {Data}");
    }
}
