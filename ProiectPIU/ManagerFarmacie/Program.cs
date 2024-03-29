using System;

class Program
{
    static void Main(string[] args)
    {
        //TEMA LABORATOR 3 SI LABORATOR 4


        Medicament[] inventarMedicamente = new Medicament[10]; // Vector pentru stocarea medicamentelor
        Vanzare[] listaVanzari = new Vanzare[10]; // Vector pentru stocarea vânzărilor

        bool continuare = true;
        while (continuare)
        {
            Console.WriteLine("=== MENIU ===");
            Console.WriteLine("1. Adăugare medicament");
            Console.WriteLine("2. Afisare stoc medicamente");
            Console.WriteLine("3. Căutare medicament");
            Console.WriteLine("4. Adăugare vânzare");
            Console.WriteLine("5. Afisare lista de vânzări");
            Console.WriteLine("6. Căutare vânzare");
            Console.WriteLine("7. Exit");
            Console.Write("Selectati opțiunea: ");
            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    AdaugaMedicament(inventarMedicamente);
                    break;
                case "2":
                    AfiseazaStocMedicamente(inventarMedicamente);
                    break;
                case "3":
                    CautaMedicament(inventarMedicamente);
                    break;
                case "4":
                    AdaugaVanzare(listaVanzari);
                    break;
                case "5":
                    AfiseazaListaVanzari(listaVanzari);
                    break;
                case "6":
                    CautaVanzare(listaVanzari);
                    break;
                case "7":
                    Console.WriteLine("La revedere!");
                    continuare = false;
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă! Vă rugăm să selectați o opțiune validă.");
                    break;
            }
        }
    }
    //Adaugare medicament in vector
    static void AdaugaMedicament(Medicament[] inventarMedicamente)
    {
        Console.WriteLine("Introduceti detalii pentru un medicament:");
        Console.Write("ID: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nume: ");
        string nume = Console.ReadLine();
        Console.Write("Descriere: ");
        string descriere = Console.ReadLine();
        Console.Write("Pret: ");
        decimal pret = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Stoc disponibil: ");
        int stoc = Convert.ToInt32(Console.ReadLine());

        int index = GasesteIndexLiber(inventarMedicamente);
        if (index != -1)
        {
            Medicament medicament = new Medicament(id, nume, descriere, pret, stoc);
            inventarMedicamente[index] = medicament;
        }
        else
        {
            Console.WriteLine("Nu mai este spațiu în stoc pentru a adăuga medicamente noi!");
        }
    }

    static int GasesteIndexLiber<T>(T[] listaObiecte)
    {
        for (int i = 0; i < listaObiecte.Length; i++)
        {
            if (listaObiecte[i] == null)
            {
                return i; // Returnăm indexul primei poziții libere
            }
        }
        return -1; // Dacă nu găsim nicio poziție liberă, returnăm -1
    }

    //Afisare medicamente din vector
    static void AfiseazaStocMedicamente(Medicament[] inventarMedicamente)
    {
        Console.WriteLine("Medicamente in stoc:");
        foreach (var med in inventarMedicamente)
        {
            if (med != null)
            {
                Console.WriteLine($"ID: {med.Id}, Nume: {med.Nume}, Pret: {med.Pret}, Stoc disponibil: {med.StocDisponibil}");
            }
        }
    }
    //Cautare medicamente dupa ID din vector
    static void CautaMedicament(Medicament[] inventarMedicamente)
    {
        Console.Write("Introduceti ID-ul medicamentului pentru a căuta: ");
        int idCautat = Convert.ToInt32(Console.ReadLine());
        Medicament medicamentCautat = null;
        foreach (var med in inventarMedicamente)
        {
            if (med != null && med.Id == idCautat)
            {
                medicamentCautat = med;
                break;
            }
        }
        if (medicamentCautat != null)
        {
            Console.WriteLine($"Medicamentul cu ID-ul {idCautat} a fost găsit. Detalii: Nume: {medicamentCautat.Nume}, Pret: {medicamentCautat.Pret}, Stoc disponibil: {medicamentCautat.StocDisponibil}");
        }
        else
        {
            Console.WriteLine($"Medicamentul cu ID-ul {idCautat} nu a fost găsit.");
        }
    }

    //Adaugare vanzare in vector
    static void AdaugaVanzare(Vanzare[] listaVanzari)
    {
        Console.WriteLine("Introduceti detalii pentru o vanzare:");
        Console.Write("ID Vanzare: ");
        int idVanzare = Convert.ToInt32(Console.ReadLine());
        Console.Write("ID Medicament: ");
        int idMedicament = Convert.ToInt32(Console.ReadLine());
        Console.Write("Cantitate: ");
        int cantitate = Convert.ToInt32(Console.ReadLine());
        Console.Write("Data: ");
        DateTime data = Convert.ToDateTime(Console.ReadLine());

        // Cautăm prima poziție liberă în vector
        int index = GasesteIndexLiber(listaVanzari);
        if (index != -1)
        {
            Vanzare vanzare = new Vanzare(idVanzare, idMedicament, cantitate, data);
            listaVanzari[index] = vanzare;
        }
        else
        {
            Console.WriteLine("Nu mai este spațiu în listă pentru a adăuga vânzări noi!");
        }
    }
    //Afisare Vanzari din vector
    static void AfiseazaListaVanzari(Vanzare[] listaVanzari)
    {
        Console.WriteLine("Lista de vânzări:");
        foreach (var vanzare in listaVanzari)
        {
            if (vanzare != null)
            {
                Console.WriteLine($"ID Vanzare: {vanzare.IdVanzare}, ID Medicament: {vanzare.IdMedicament}, Cantitate: {vanzare.Cantitate}, Data: {vanzare.Data}");
            }
        }
    }
    //Cautare vanzare dupa ID din vector
    static void CautaVanzare(Vanzare[] listaVanzari)
    {
        Console.Write("Introduceti ID-ul vânzării pentru a căuta: ");
        int idVanzareCautata = Convert.ToInt32(Console.ReadLine());
        Vanzare vanzareCautata = null;
        foreach (var vanzare in listaVanzari)
        {
            if (vanzare != null && vanzare.IdVanzare == idVanzareCautata)
            {
                vanzareCautata = vanzare;
                break;
            }
        }
        if (vanzareCautata != null)
        {
            Console.WriteLine($"Vânzarea cu ID-ul {idVanzareCautata} a fost găsită. Detalii: ID Medicament: {vanzareCautata.IdMedicament}, Cantitate: {vanzareCautata.Cantitate}, Data: {vanzareCautata.Data}");
        }
        else
        {
            Console.WriteLine($"Vânzarea cu ID-ul {idVanzareCautata} nu a fost găsită.");
        }
    }
}
