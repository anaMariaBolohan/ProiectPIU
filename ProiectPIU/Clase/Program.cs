using System;
//TEMA LABORATOR 2
public class Medicament
{
    //TEMA LABORATOR 3 (Proprietati auto-implemented)
    public int Id { get; set; }
    public string Nume { get; set; }
    public string Descriere { get; set; }
    public decimal Pret { get; set; }
    public int StocDisponibil { get; set; }

    public Medicament(int id, string nume, string descriere, decimal pret, int stocDisponibil)
    {
        Id = id;
        Nume = nume;
        Descriere = descriere;
        Pret = pret;
        StocDisponibil = stocDisponibil;
    }

    public void AfiseazaDetalii()
    {
        Console.WriteLine($"ID: {Id}, Nume: {Nume}, Descriere: {Descriere}, Pret: {Pret}, Stoc disponibil: {StocDisponibil}");
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
