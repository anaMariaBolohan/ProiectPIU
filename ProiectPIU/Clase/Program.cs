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

    //constante
    private const char SEPARATOR_PRINCIPAL_FISIER = '$';
    private const char SEPARATOR_SECUNDAR_FISIER = ' ';
    private const bool SUCCES = true;
    
    private const int ID = 0;
    private const int NUME = 1;
    private const int DESCRIERE = 2;
    private const int PRET = 3;
    private const int STOCDISPONIBIL = 4;
    public Medicament(int id, string nume, string descriere, decimal pret, int stocDisponibil)
    {
        Id = id;
        Nume = nume;
        Descriere = descriere;
        Pret = pret;
        StocDisponibil = stocDisponibil;
    }

    public string ConversieLaSir_PentruFisier()
    {
        string obiectMedicamentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
            SEPARATOR_PRINCIPAL_FISIER,
            Id.ToString(),
            (Nume ?? " NECUNOSCUT "),
            (Descriere ?? " NECUNOSCUT "),
            (Pret.ToString() ?? " NECUNOSCUT "),
            (StocDisponibil.ToString() ?? " NECUNOSCUT ")
            );

        return obiectMedicamentPentruFisier;
    }

    public Medicament(string linieFisier)
    {
        string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

        //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier prin apelul implicit al metodei ConversieLaSir_PentruFisier()
        this.Id = Convert.ToInt32(dateFisier[ID]);
        this.Nume = dateFisier[NUME];
        this.Descriere = dateFisier[DESCRIERE];
        this.Pret = Convert.ToInt32(dateFisier[PRET]);
        this.StocDisponibil = Convert.ToInt32(dateFisier[STOCDISPONIBIL]);
    }

    public void copyMedicament(Medicament medicament)
    {
        this.Id = medicament.Id;
        this.Nume = medicament.Nume;
        this.Descriere = medicament.Descriere;
        this.Pret = medicament.Pret;
        this.StocDisponibil = medicament.StocDisponibil;

    }
    public Medicament()
    {
        this.Id = 0;
        this.Nume = string.Empty;
        this.Descriere = string.Empty;
        this.Pret = 0;
        this.StocDisponibil = 0;
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
