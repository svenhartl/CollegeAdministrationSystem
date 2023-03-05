

/* ---------------------------
 
Autor: Sven Hartl
Projekt: Referada
Predmet: Osnove programiranja
Ustanova: VUV
Godina: 2023.
 
 --------------------------- */




using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security;
using ConsoleTables;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using System.ComponentModel.Design;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace KV
{


    public struct Users
    {

        public int id;
        public string username;
        public string password;
        public string type;

        public Users(int Id, string Username, string Password,string Type)
        {

            id = Id;
            username = Username;
            password = Password;
            type = Type;
        }
    };

    public struct Studiji
    {

        public int id;
        public string naziv_studija;
        public int godina;

        public Studiji(int Id, string Naziv_studija, int Godina)
        {

            id = Id;
            naziv_studija = Naziv_studija;
            godina = Godina;
        }


    };
    public struct Smjerovi
    {

        public int id;
        
        public string naziv_smjera;
      

        public Smjerovi(int Id, string Naziv_smjera)
        {

            id = Id;
            naziv_smjera = Naziv_smjera;
          
        }


    };

    public struct Students
    {

        public int id;
        public string jmbag;
        public string surname;
        public string name;
        public List<StudijS> studij;
        public List<SmjerS> smjer;
        public int godina;
        public string spol;


        public Students(int Id, string Jmbag, string Surname, string Name,List<StudijS> Studij,List<SmjerS> Smjer,int Godina,string Spol)
        {

            id = Id;
            jmbag = Jmbag;
            surname = Surname;
            name = Name;
            studij = Studij;
            smjer = Smjer;
            godina = Godina;
            spol = Spol;
        }
    };

    public struct StudijS {

        public int id;

        public StudijS(int Id) {

            id = Id;
        }

    };

    public struct SmjerS
    {

        public int id;
        

        public SmjerS(int Id)
        {

            id = Id;
            
        }

    };

    public struct PredmetS
    {

        public int id;


        public PredmetS(int Id)
        {

            id = Id;

        }

    };
    class Program
    {


        public static void MainMenu(string username, List<Studiji> lStudiji, List<Students> lStudents, List<Users> lUsers, dynamic students_json,string type,List<Smjerovi> lSmjerovi, List<PredmetS> lPredmeti)
        {

            //IZBORNIK ZA ADMINA

            {
                Console.Clear();
                Console.WriteLine();
                string textToEnter = "GLAVNI IZBORNIK";
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));

                Console.WriteLine();
                Console.WriteLine("Prijavljen korisnik: {0}, {1}", username, type);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1 - Prikazi sve smjerove");
                Console.WriteLine("2 - Prikazi sve studente");
                Console.WriteLine("3 - Pronadi studenta");
                Console.WriteLine("4 - Dodaj studenta");
                Console.WriteLine("5 - Obrisi studenta");
                Console.WriteLine("6 - Prebaci studenta");
                Console.WriteLine("7 - Statistika");
                Console.WriteLine("8 - Odjava");

                Console.WriteLine();

                Console.Write("Izbor: ");
                int op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {

                    case 1:
                        Console.Clear();
                        Console.WriteLine();
                        string texT = "SMJEROVI";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (texT.Length / 2)) + "}", texT));
                        Console.WriteLine();
                        var studiji_table = new ConsoleTable("R.br.", "ID", "Naziv smjera","Godina");
                        foreach (var ss in lSmjerovi)
                        {

                            foreach (var s in lStudiji)
                            {
                                if (s.id == ss.id)
                                {
                                    studiji_table.AddRow(ss.id, ss.id, ss.naziv_smjera, s.godina);
                                }
                                
                            }

                            

                        }
                        studiji_table.Write();
                        Console.WriteLine();
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Escape)
                        {

                            MainMenu(username, lStudiji, lStudents, lUsers, students_json,type,lSmjerovi,lPredmeti);
                        }
                        break;
                    case 2:

                        Console.Clear();
                        Console.WriteLine();
                        string texTt = "STUDENTI";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (texTt.Length / 2)) + "}", texTt));
                        Console.WriteLine();
                        var studenti_table = new ConsoleTable("ID", "JMBAG", "Prezime", "Ime", "Studij", "Smjer", "Godina");

                        foreach (var v in lStudents) {
                            

                            foreach (var s in lStudiji)
                            {
                                foreach (var i in v.studij) {
                                    foreach (var ss in lSmjerovi) {
                                        foreach (var vv in v.smjer) {
                                            
                                            if (i.id == s.id && ss.id==vv.id)
                                            {
                                                
                                                    studenti_table.AddRow(v.id, v.jmbag, v.surname, v.name, s.naziv_studija, ss.naziv_smjera, v.godina);
                                            

                                            }
                                          
                                        }
                                      
                                           
                                        
                                       
                                    }

                                       
                                    
                                 
                                }
                              

                            }
                        }

                        studenti_table.Write();
                       
                        Console.WriteLine();
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        ConsoleKeyInfo keyy = Console.ReadKey(true);



                        if (keyy.Key == ConsoleKey.Escape)
                        {

                            MainMenu(username, lStudiji, lStudents, lUsers, students_json,type,lSmjerovi,lPredmeti);
                        }

                        break;
                    case 3:
                       
                            ConsoleKeyInfo keyyy;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine();
                                string textToEnterr = "PRETRAGA STUDENTA";
                                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterr.Length / 2)) + "}", textToEnterr));
                                Console.WriteLine();
                                Students search1 = new Students();
                                Console.Write("Pretrazi studenta po prezimenu: ");
                                search1.surname = Console.ReadLine();
                                Console.WriteLine();
                                SearchLogg(search1, lStudents, lStudiji, lSmjerovi);
                                Console.WriteLine();
                                Console.WriteLine("ENTER za ponovno pretrazivanje");
                                Console.WriteLine("ESC za povratak na glavni izbornik");
                                keyyy = Console.ReadKey(true);

                                if (keyyy.Key == ConsoleKey.Escape)
                                {

                                    MainMenu(username, lStudiji, lStudents, lUsers, students_json, type, lSmjerovi,lPredmeti);
                                }
                            } while (keyyy.Key == ConsoleKey.Enter);
                        
                      
                        break;
                    case 4:
                        ConsoleKeyInfo keey;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine();
                            string textToEnterr = "DODAVANJE STUDENTA";
                            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterr.Length / 2)) + "}", textToEnterr));
                            Console.WriteLine();

                            AddStudent(lStudents, students_json,lStudiji,lSmjerovi,lPredmeti);
                            Console.WriteLine();
                            Console.WriteLine("ENTER za ponovno dodavanje");
                            Console.WriteLine("ESC za povratak na glavni izbornik");
                            keey = Console.ReadKey(true);

                            if (keey.Key == ConsoleKey.Escape)
                            {

                                MainMenu(username, lStudiji, lStudents, lUsers, students_json, type,lSmjerovi,lPredmeti);
                            }
                        } while (keey.Key == ConsoleKey.Enter);
                        break;
                    case 5:
                        ConsoleKeyInfo keeyy;
                        do
                        {
                            Console.Clear();
                        Console.WriteLine();
                        string textToEnterr1 = "BRISANJE STUDENTA";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterr1.Length / 2)) + "}", textToEnterr1));
                        Console.WriteLine();
                        DeleteStudent(lStudents);
                            Console.WriteLine();
                            Console.WriteLine("ENTER za ponovno brisanje");
                            Console.WriteLine("ESC za povratak na glavni izbornik");
                            keeyy = Console.ReadKey(true);

                            if (keeyy.Key == ConsoleKey.Escape)
                            {

                                MainMenu(username, lStudiji, lStudents, lUsers, students_json, type,lSmjerovi,lPredmeti);
                            }
                        } while (keeyy.Key == ConsoleKey.Enter);
                        break;

                    case 6:
                        ConsoleKeyInfo keeyy2;
                        do { 
                        Console.Clear();
                        Console.WriteLine();
                        string textToEnterrr2 = "PREBACI STUDENTA";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterrr2.Length / 2)) + "}", textToEnterrr2));
                        Console.WriteLine();
                        PrebaciStudenta(lStudents,lStudiji,lSmjerovi);
                        Console.WriteLine();
                        Console.WriteLine("ENTER za ponovno prebacivanje");
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        keeyy2 = Console.ReadKey(true);

                        if (keeyy2.Key == ConsoleKey.Escape)
                        {
                            MainMenu(username, lStudiji, lStudents, lUsers, students_json, type, lSmjerovi,lPredmeti);
                        }

                } while (keeyy2.Key == ConsoleKey.Enter) ;
                break;

                    case 7:

                       ConsoleKeyInfo keeyyy;
                         
                        Console.Clear();
                        Console.WriteLine();
                        string textToEnterrr1 = "STATISTIKA";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterrr1.Length / 2)) + "}", textToEnterrr1));
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Ukupan broj studija: {0}",lStudiji.Count()-1);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Ukupan broj smjerova: {0}", lSmjerovi.Count());

                        Console.WriteLine();


                        Console.WriteLine("Ukupan broj studenata: {0}",lStudents.Count());
                        Console.WriteLine();
                        int men = 0;
                        int elek = 0;
                        int pod = 0;
                        int rac = 0;
                        foreach (var s in lStudents) {
                            
                            foreach (var st in s.studij) {
                                
                                if (st.id==1 || st.id==2) {

                                    men+=1;
                                }
                                if (st.id == 3)
                                {

                                    elek+=1;
                                }
                                if (st.id == 4)
                                {

                                    pod += 1;
                                }
                                if (st.id == 5)
                                {

                                    rac += 1;
                                }


                            }
                        }

                        if (men > elek && men > pod && men > rac) {

                            Console.WriteLine("Studij sa najvise studenata je Menadzent, {0}",men);
                        }
                        if (elek > men && elek > pod && elek > rac)
                        {

                            Console.WriteLine("Studij sa najvise studenata je Elektrotehnika, {0}", elek);
                        }
                        if (pod > men && pod > elek && pod > rac)
                        {

                            Console.WriteLine("Studij sa najvise studenata je POduzetnistvo, {0}", pod);
                        }
                        if (rac > elek && rac > pod && rac > men)
                        {

                            Console.WriteLine("Studij sa najvise studenata je Racunarstvo, {0}", rac);
                        }
                        if (men == elek && men>rac && men>pod) {

                            Console.WriteLine("Studiji sa najvise studenata su Menadzment i Elektrotehnika, {0}",men);
                        }
                        if (men == pod && men>elek && men>rac)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Menadzment i Poduzetnistvo, {0}", men);
                        }
                        if (men == rac && men> elek && men>pod)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Menadzment i Racunarstvo, {0}", men);
                        }
                        if (elek == pod && elek>men && elek>rac)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Elektrotehnika i Poduzetnistvo, {0}", elek);
                        }
                        if (elek == rac && elek>men && elek>pod)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Elektrotehnika i Racunarstvo, {0}", elek);
                        }
                        if (pod == rac && pod>men && pod>elek)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Poduzetnistvo i Racunarstvo, {0}", pod);
                        }
                        Console.WriteLine();

                        Console.WriteLine("Ukupan broj studenata po studiju: ");
                        Console.WriteLine();
                        Console.WriteLine("Menadzment: {0}",men);
                        Console.WriteLine("Elektrotehnika: {0}",elek);
                        Console.WriteLine("Poduzetnistvo: {0}",pod);
                        Console.WriteLine("Racunarstvo: {0}",rac);
                        Console.WriteLine();

                        Console.WriteLine("Ukupan broj studenata po godini: ");

                        int p = 0;
                        int d = 0;
                        int t = 0;
                        double m = 0, z = 0;
                        double posz;
                        double posm;
                        foreach (var s in lStudents)
                        {

                            

                                if (s.godina==1)
                                {

                                    p += 1;
                                }
                                if (s.godina == 2)
                                {

                                    d += 1;
                                }
                                if (s.godina == 3)
                                {

                                    t += 1;
                                }

                            

                            if (s.spol == "m") {
                                m+=1;
                            }
                            if (s.spol == "z")
                            {
                                z+=1;
                            }

                            

                            
                        }
                        posz = (z / lStudents.Count) * 100;
                        posm = (m / lStudents.Count) * 100;
                        Console.WriteLine();
                        Console.WriteLine("Prva godina: {0}", p);
                        Console.WriteLine("Druga godina: {0}", d);
                        Console.WriteLine("Treca godina: {0}", t);
                        Console.WriteLine();
                        Console.WriteLine("Postotak muskaraca: {0:N2}%", posm);
                        Console.WriteLine("Postotak zena: {0:N2}%", posz);
                        Console.WriteLine();
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        Console.WriteLine("Unesite studij koji zelite pregledati: ");
                        Console.WriteLine("");
                        int a;
                        a = Convert.ToInt32(Console.ReadLine());

                        var s_table = new ConsoleTable("R.br.", "ID", "Naziv smjera", "Godina");

                        foreach (var v in lStudents)
                        {


                            foreach (var s in lStudiji)
                            {
                                foreach (var i in v.studij)
                                {
                                    foreach (var ss in lSmjerovi)
                                    {
                                        foreach (var vv in v.smjer)
                                        {

                                            if (i.id == a && ss.id == a)
                                            {

                                                s_table.AddRow(v.id, v.jmbag, v.surname, v.name, s.naziv_studija, ss.naziv_smjera, v.godina);


                                            }

                                        }




                                    }




                                }


                            }
                        }

                        s_table.Write();

                        keeyyy = Console.ReadKey(true);

                        if (keeyyy.Key == ConsoleKey.Escape)
                        {

                            MainMenu(username, lStudiji, lStudents, lUsers, students_json, type, lSmjerovi,lPredmeti);
                        }

                      

                        break;
                    case 8:
                        Console.Clear();
                        UserLog(lUsers, lStudiji, lStudents, students_json,lSmjerovi,lPredmeti);


                        break;

                }
            }
        }

        public static void MainMenuUser(string username, List<Studiji> lStudiji, List<Students> lStudents, List<Users> lUsers, dynamic students_json,string type,List<Smjerovi> lSmjerovi, List<PredmetS> lPredmeti)
        {
            //IZBORNIK ZA KORISNIKA

            {
                Console.Clear();
                Console.WriteLine();
                string textToEnter = "GLAVNI IZBORNIK";
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));

                Console.WriteLine();
                Console.WriteLine("Prijavljen korisnik: {0}, {1}", username, type);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1 - Prikazi sve smjerove");
                Console.WriteLine("2 - Prikazi sve studente");
                Console.WriteLine("3 - Pronadi studenta");
                Console.WriteLine("7 - Statistika");
                Console.WriteLine("8 - Odjava");

                Console.WriteLine();

                Console.Write("Izbor: ");
                int op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine();
                        string texT = "SMJEROVI";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (texT.Length / 2)) + "}", texT));
                        Console.WriteLine();
                        var studiji_table = new ConsoleTable("R.br.", "ID", "Naziv smjera", "Godina");
                        foreach (var ss in lSmjerovi)
                        {
                            foreach (var s in lStudiji)
                            {
                                if (s.id == ss.id)
                                {
                                    studiji_table.AddRow(ss.id, ss.id, ss.naziv_smjera, s.godina);
                                }

                            }
                        }
                        studiji_table.Write();
                        Console.WriteLine();
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Escape)
                        {
                            MainMenuUser(username, lStudiji, lStudents, lUsers, students_json, type,lSmjerovi, lPredmeti);
                        }

                        break;

                    case 2:

                        Console.Clear();
                        Console.WriteLine();
                        string texTt = "STUDENTI";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (texTt.Length / 2)) + "}", texTt));
                        Console.WriteLine();
                        var studenti_table = new ConsoleTable("ID", "JMBAG", "Prezime", "Ime", "Studij", "Smjer", "Godina");

                        foreach (var v in lStudents)
                        {
                            foreach (var s in lStudiji)
                            {
                                foreach (var ss in lSmjerovi) {
                                    foreach (var i in v.studij)
                                    {
                                        foreach (var sm in v.smjer) {
                                            if (i.id == s.id && ss.id == sm.id) {
                                               
                                                studenti_table.AddRow(v.id, v.jmbag, v.surname, v.name, s.naziv_studija, ss.naziv_smjera, v.godina);
                                            }
                                           

                                        }
                                     }

                                }
                             }
                        }

                        studenti_table.Write();

                       
                        Console.WriteLine();
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        ConsoleKeyInfo keyy = Console.ReadKey(true);

                        if (keyy.Key == ConsoleKey.Escape)
                        {
                            MainMenuUser(username, lStudiji, lStudents, lUsers, students_json,type,lSmjerovi, lPredmeti);
                        }

                        break;

                    case 3:

                        ConsoleKeyInfo keyyy;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine();
                            string textToEnterr = "PRETRAGA STUDENTA";
                            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterr.Length / 2)) + "}", textToEnterr));
                            Console.WriteLine();
                            Students search1 = new Students();
                            Console.Write("Pretrazi studenta po prezimenu: ");
                            search1.surname = Console.ReadLine();
                            Console.WriteLine();
                            SearchLogg(search1, lStudents, lStudiji,lSmjerovi);
                            Console.WriteLine();
                            Console.WriteLine("ENTER za ponovno pretrazivanje");
                            Console.WriteLine("ESC za povratak na glavni izbornik");
                            keyyy = Console.ReadKey(true);

                            if (keyyy.Key == ConsoleKey.Escape)
                            {
                                MainMenuUser(username, lStudiji, lStudents, lUsers, students_json, type,lSmjerovi, lPredmeti);
                            }

                        } while (keyyy.Key == ConsoleKey.Enter);

                        break;
                    case 7:

                        ConsoleKeyInfo keeyyy;

                        Console.Clear();
                        Console.WriteLine();
                        string textToEnterrr1 = "STATISTIKA";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterrr1.Length / 2)) + "}", textToEnterrr1));
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Ukupan broj studija: {0}", lStudiji.Count() - 1);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Ukupan broj smjerova: {0}", lSmjerovi.Count());

                        Console.WriteLine();


                        Console.WriteLine("Ukupan broj studenata: {0}", lStudents.Count());
                        Console.WriteLine();
                        int men = 0;
                        int elek = 0;
                        int pod = 0;
                        int rac = 0;
                        foreach (var s in lStudents)
                        {

                            foreach (var st in s.studij)
                            {

                                if (st.id == 1 || st.id == 2)
                                {

                                    men += 1;
                                }
                                if (st.id == 3)
                                {

                                    elek += 1;
                                }
                                if (st.id == 4)
                                {

                                    pod += 1;
                                }
                                if (st.id == 5)
                                {

                                    rac += 1;
                                }


                            }
                        }

                        if (men > elek && men > pod && men > rac)
                        {

                            Console.WriteLine("Studij sa najvise studenata je Menadzent, {0}", men);
                        }
                        if (elek > men && elek > pod && elek > rac)
                        {

                            Console.WriteLine("Studij sa najvise studenata je Elektrotehnika, {0}", elek);
                        }
                        if (pod > men && pod > elek && pod > rac)
                        {

                            Console.WriteLine("Studij sa najvise studenata je POduzetnistvo, {0}", pod);
                        }
                        if (rac > elek && rac > pod && rac > men)
                        {

                            Console.WriteLine("Studij sa najvise studenata je Racunarstvo, {0}", rac);
                        }
                        if (men == elek && men > rac && men > pod)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Menadzment i Elektrotehnika, {0}", men);
                        }
                        if (men == pod && men > elek && men > rac)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Menadzment i Poduzetnistvo, {0}", men);
                        }
                        if (men == rac && men > elek && men > pod)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Menadzment i Racunarstvo, {0}", men);
                        }
                        if (elek == pod && elek > men && elek > rac)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Elektrotehnika i Poduzetnistvo, {0}", elek);
                        }
                        if (elek == rac && elek > men && elek > pod)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Elektrotehnika i Racunarstvo, {0}", elek);
                        }
                        if (pod == rac && pod > men && pod > elek)
                        {

                            Console.WriteLine("Studiji sa najvise studenata su Poduzetnistvo i Racunarstvo, {0}", pod);
                        }
                        Console.WriteLine();

                        Console.WriteLine("Ukupan broj studenata po studiju: ");
                        Console.WriteLine();
                        Console.WriteLine("Menadzment: {0}", men);
                        Console.WriteLine("Elektrotehnika: {0}", elek);
                        Console.WriteLine("Poduzetnistvo: {0}", pod);
                        Console.WriteLine("Racunarstvo: {0}", rac);
                        Console.WriteLine();

                        Console.WriteLine("Ukupan broj studenata po godini: ");

                        int p = 0;
                        int d = 0;
                        int t = 0;
                        double m = 0, z = 0;
                        double posz;
                        double posm;
                        foreach (var s in lStudents)
                        {



                            if (s.godina == 1)
                            {

                                p += 1;
                            }
                            if (s.godina == 2)
                            {

                                d += 1;
                            }
                            if (s.godina == 3)
                            {

                                t += 1;
                            }

                            if (s.spol == "m")
                            {
                                m+=1;
                            }
                            if (s.spol == "z")
                            {
                                z+=1;
                            }

                            
                        }
                        posz = (z / lStudents.Count) * 100;
                        posm = (m / lStudents.Count) * 100;
                        Console.WriteLine();
                        Console.WriteLine("Prva godina: {0}", p);
                        Console.WriteLine("Druga godina: {0}", d);
                        Console.WriteLine("Treca godina: {0}", t);
                        Console.WriteLine();
                        Console.WriteLine("Postotak muskaraca: {0:N2}%", posm);
                        Console.WriteLine("Postotak zena: {0:N2}%", posz);
                        Console.WriteLine();
                        Console.WriteLine("ESC za povratak na glavni izbornik");
                        keeyyy = Console.ReadKey(true);

                        if (keeyyy.Key == ConsoleKey.Escape)
                        {

                            MainMenuUser(username, lStudiji, lStudents, lUsers, students_json, type, lSmjerovi, lPredmeti);
                        }
                        break;
                    case 8:

                        Console.Clear();
                        UserLog(lUsers, lStudiji, lStudents, students_json,lSmjerovi,lPredmeti);

                        break;

                }
            }
        }

        public static void PrebaciStudenta(List<Students> lStudents,List<Studiji> lStudiji, List<Smjerovi> lSmjerovi) {

            //PREBACIVANJE STUDENATA

            int sid;
            bool b = false;
            char ch;

            string ime, prezime;

            Console.WriteLine();
            var studenti_table = new ConsoleTable("ID", "JMBAG", "Prezime", "Ime", "Studij", "Smjer", "Godina");

            foreach (var v in lStudents)
            {
                foreach (var s in lStudiji)
                {
                    foreach (var ss in lSmjerovi)
                    {
                        foreach (var i in v.studij)
                        {
                            foreach (var sm in v.smjer)
                            {
                                if (i.id == s.id && ss.id == sm.id)
                                {

                                    studenti_table.AddRow(v.id, v.jmbag, v.surname, v.name, s.naziv_studija, ss.naziv_smjera, v.godina);
                                }
                            }
                        }

                    }
                }
            }

            studenti_table.Write();

            List<SmjerS> nsmjer=new List<SmjerS>();
            Console.WriteLine();
                Console.WriteLine("Unesite ime i prezime ucenika kojeg zelite prebaciti: ");
            Console.WriteLine();
                Console.Write("Ime: ");
                ime = Convert.ToString(Console.ReadLine());
            Console.WriteLine();
                Console.Write("Prezime: ");
                prezime = Convert.ToString(Console.ReadLine());
            Console.WriteLine();


            

            foreach (var student in lStudents.ToList())
            {

                if (student.name.ToUpper() == ime.ToUpper() && student.surname.ToUpper() == prezime.ToUpper())
                {
                    b = true;
                    Console.WriteLine("Jeste li sigurni da zelite prebaciti studenta {0} {1}? (y/n)", student.name, student.surname);
                    ch = Convert.ToChar(Console.ReadLine());
                    if(ch=='n' || ch!='y')
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Prebacivanje otkazano.");
                        Console.WriteLine();
                        PrebaciStudenta(lStudents, lStudiji, lSmjerovi);
                    }
                    Console.WriteLine("Odaberite smjer u koji zelite prebaciti studenta: ");

                    foreach (var smjer in student.smjer)
                    {
                        
                        int unos;
                        
                            if (smjer.id == 1)
                            {

                            do {
                                
                                Console.WriteLine("1 - Informaticki menadzment (Trenutni smjer)");
                                Console.WriteLine("2 - Menadzment ruralnog turizma");
                                Console.WriteLine("3 - Telekomunikacije i informatika");
                                Console.WriteLine("4 - Poduzetnistvo usluga");
                                Console.WriteLine("5 - programsko inzenjerstvo");
                                Console.WriteLine();
                                Console.Write("Unos: ");
                                unos = Convert.ToInt32(Console.ReadLine());
                                if (unos < 0 || unos > 5) {
                                    Console.WriteLine();
                                    Console.WriteLine("Smjer ne postoji, pokusajte ponovo");
                                    Console.WriteLine();
                                }
                            } while (unos<0 || unos>5);
                                List < StudijS > nstudij = new List<StudijS>();
                                StudijS studij = new StudijS();
                                if (unos == 1)
                                {

                                    studij = new StudijS(1);

                                }
                                if (unos == 2)
                                {

                                    studij = new StudijS(2);

                                }
                                if (unos == 3)
                                {

                                    studij = new StudijS(3);

                                }
                                if (unos == 4)
                                {

                                    studij = new StudijS(4);

                                }
                                if (unos == 5)
                                {

                                    studij = new StudijS(5);

                                }
                           
                            SmjerS smjern = new SmjerS(unos);
                            nstudij.Add(studij);
                            nsmjer.Add(smjern);
                            Students nstudent = new Students(student.id, student.jmbag, student.surname, student.name, nstudij, nsmjer, 1,student.spol);
                            lStudents.Remove(student);
                            lStudents.Add(nstudent);
                            dynamic json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
                            System.IO.File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);
                            Console.WriteLine();
                            Console.WriteLine("Izvrseno");
                        }


                        if (smjer.id == 2)
                        {
                            do { 
                            Console.WriteLine("1 - Informaticki menadzment");
                            Console.WriteLine("2 - Menadzment ruralnog turizma (Trenutni smjer)");
                            Console.WriteLine("3 - Telekomunikacije i informatika");
                            Console.WriteLine("4 - Poduzetnistvo usluga");
                            Console.WriteLine("5 - Programsko inzenjerstvo");
                            Console.WriteLine();
                            Console.Write("Unos: ");
                            unos = Convert.ToInt32(Console.ReadLine());
                                if (unos < 0 || unos > 5)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Smjer ne postoji, pokusajte ponovo");
                                    Console.WriteLine();
                                }
                            } while (unos < 0 || unos > 5);
                            List<StudijS> nstudij = new List<StudijS>();
                            StudijS studij = new StudijS();
                            if (unos == 1)
                            {

                                studij = new StudijS(1);

                            }
                            if (unos == 2)
                            {

                                studij = new StudijS(2);

                            }
                            if (unos == 3)
                            {

                                studij = new StudijS(3);

                            }
                            if (unos == 4)
                            {

                                studij = new StudijS(4);

                            }
                            if (unos == 5)
                            {

                                studij = new StudijS(5);

                            }


                            nstudij.Add(studij);
                            SmjerS smjern = new SmjerS(unos);
                            nsmjer.Add(smjern);
                            Students nstudent = new Students(student.id, student.jmbag, student.surname, student.name, nstudij, nsmjer, 1,student.spol);
                            lStudents.Remove(student);
                            lStudents.Add(nstudent);
                            dynamic json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
                            System.IO.File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);
                            Console.WriteLine();
                            Console.WriteLine("Izvrseno");
                        }

                        if (smjer.id == 3)
                        {
                            do { 
                            Console.WriteLine("1 - Informaticki menadzment");
                            Console.WriteLine("2 - Menadzment ruralnog turizma");
                            Console.WriteLine("3 - Telekomunikacije i informatika (Trenutni smjer)");
                            Console.WriteLine("4 - Poduzetnistvo usluga");
                            Console.WriteLine("5 - Programsko inzenjerstvo");
                            Console.WriteLine();
                            Console.Write("Unos: ");
                            unos = Convert.ToInt32(Console.ReadLine());
                                if (unos < 0 || unos > 5)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Smjer ne postoji, pokusajte ponovo");
                                    Console.WriteLine();
                                }
                            } while (unos < 0 || unos > 5);
                            List<StudijS> nstudij = new List<StudijS>();
                            StudijS studij = new StudijS();
                            if (unos == 1)
                            {

                                studij = new StudijS(1);

                            }
                            if (unos == 2)
                            {

                                studij = new StudijS(2);

                            }
                            if (unos == 3)
                            {

                                studij = new StudijS(3);

                            }
                            if (unos == 4)
                            {

                                studij = new StudijS(4);

                            }
                            if (unos == 5)
                            {

                                studij = new StudijS(5);

                            }

                            nstudij.Add(studij);
                            SmjerS smjern = new SmjerS(unos);
                            nsmjer.Add(smjern);
                            Students nstudent = new Students(student.id, student.jmbag, student.surname, student.name, nstudij, nsmjer, 1, student.spol);
                            lStudents.Remove(student);
                            lStudents.Add(nstudent);
                            dynamic json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
                            System.IO.File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);
                            Console.WriteLine();
                            Console.WriteLine("Izvrseno");
                        }

                        if (smjer.id == 4)
                        {
                            do { 
                            Console.WriteLine("1 - Informaticki menadzment");
                            Console.WriteLine("2 - Menadzment ruralnog turizma");
                            Console.WriteLine("3 - Telekomunikacije i informatika");
                            Console.WriteLine("4 - Poduzetnistvo usluga (Trenutni smjer)");
                            Console.WriteLine("5 - Programsko inzenjerstvo");
                            Console.WriteLine();
                            Console.Write("Unos: ");
                            unos = Convert.ToInt32(Console.ReadLine());
                                if (unos < 0 || unos > 5)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Smjer ne postoji, pokusajte ponovo");
                                    Console.WriteLine();
                                }
                            } while (unos < 0 || unos > 5);
                            List<StudijS> nstudij = new List<StudijS>();
                            StudijS studij = new StudijS();
                            if (unos == 1)
                            {

                                studij = new StudijS(1);

                            }
                            if (unos == 2)
                            {

                                studij = new StudijS(2);

                            }
                            if (unos == 3)
                            {

                                studij = new StudijS(3);

                            }
                            if (unos == 4)
                            {

                                studij = new StudijS(4);

                            }
                            if (unos == 5)
                            {

                                studij = new StudijS(5);

                            }


                            nstudij.Add(studij);
                            SmjerS smjern = new SmjerS(unos);
                            nsmjer.Add(smjern);
                            Students nstudent = new Students(student.id, student.jmbag, student.surname, student.name, nstudij, nsmjer, 1, student.spol);
                            lStudents.Remove(student);
                            lStudents.Add(nstudent);
                            dynamic json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
                            System.IO.File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);
                            Console.WriteLine();
                            Console.WriteLine("Izvrseno");
                        }
                        if (smjer.id == 5)
                        {


                            do {
                                
                                Console.WriteLine("1 - Informaticki menadzment");
                            Console.WriteLine("2 - Menadzment ruralnog turizma");
                            Console.WriteLine("3 - Telekomunikacije i informatika");
                            Console.WriteLine("4 - Poduzetnistvo usluga");
                            Console.WriteLine("5 - Programsko inzenjerstvo (Trenutni smjer)");
                            Console.WriteLine();
                            Console.Write("Unos: ");
                            unos = Convert.ToInt32(Console.ReadLine());
                                if (unos < 0 || unos > 5)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Smjer ne postoji, pokusajte ponovo");
                                    Console.WriteLine();
                                }
                            } while (unos < 0 || unos > 5);
                            List<StudijS> nstudij = new List<StudijS>();
                            StudijS studij = new StudijS();
                            if (unos == 1)
                            {

                                studij = new StudijS(1);

                            }
                            if (unos == 2)
                            {

                                studij = new StudijS(2);

                            }
                            if (unos == 3)
                            {

                                studij = new StudijS(3);

                            }
                            if (unos == 4)
                            {

                                studij = new StudijS(4);

                            }
                            if (unos == 5)
                            {

                                studij = new StudijS(5);

                            }

                            SmjerS smjern = new SmjerS(unos);
                            nstudij.Add(studij);
                            nsmjer.Add(smjern);
                            Students nstudent = new Students(student.id, student.jmbag, student.surname, student.name, nstudij, nsmjer, 1, student.spol);
                            lStudents.Remove(student);
                            lStudents.Add(nstudent);
                            dynamic json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
                            System.IO.File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);
                            Console.WriteLine();
                            Console.WriteLine("Izvrseno");
                        }

                    }
                    if (ch == 'y')
                    {
                        lStudents.Remove(student);
                    }
                    
                }
             
            }
            if (b == false)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Student nije pronaden.");
                Console.WriteLine();
                PrebaciStudenta(lStudents, lStudiji, lSmjerovi);
            }

        }

        public static void DeleteStudent(List<Students> lStudents) {

            //BRISANJE STUDENATA

            int sid;
            char ch;
            bool b=false;
            do
            {
                Console.WriteLine("Unesi id ucenika kojeg zelite izbrisati: ");
                sid = Convert.ToInt32(Console.ReadLine());

            } while (sid > lStudents.Count() && sid <= 0);


            foreach (var student in lStudents.ToList()) {

                if (student.id == sid)
                {
                    b = true;
                    Console.WriteLine("Jeste li sigurni da zelite obrisati studenta {0} {1} - {2}? (y/n)", student.name, student.surname, student.jmbag);
                    ch = Convert.ToChar(Console.ReadLine());
                    if (ch == 'y')
                    {
                        lStudents.Remove(student);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Brisanje otkazano");
                    }
                }
            }

            if (b == false) {

              Console.WriteLine("Student nije pronaden");
                DeleteStudent(lStudents);      
            }

            dynamic json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
            System.IO.File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);

        }

        public static void SearchLog(Students search, List<Students> lStudents,List<Studiji> lStudiji, List<Smjerovi> lSmjerovi)
        {
            //PRETRAGA STUDENTA

            bool b = false;
            foreach (var a in lStudents)
            {
                foreach (var s in lStudiji)
                {
                    foreach (var i in a.studij)
                    {
                        foreach (var ss in lSmjerovi) { 
                        if (a.surname.ToUpper().Contains(search.surname.ToUpper()) && i.id == s.id)
                        {
                               
                            Console.WriteLine("Ime i prezime: {0} {1}", a.name, a.surname);
                            Console.WriteLine("JMBAG: {0}", a.jmbag);

                            Console.WriteLine("Upisani studij: {0}", s.naziv_studija);
                            Console.WriteLine("Upisani smjer: {0}", ss.naziv_smjera);


                            Console.WriteLine("Godina: {0}", a.godina);
                            Console.WriteLine();
                            b = true;
                        }
                    }
                 }
                   
             }
                   
         }
            if (b == false)
            {
                Console.WriteLine("Student nije pronaden");
                
            }
}

        public static void SearchLogg(Students search1, List<Students> lStudents, List<Studiji> lStudiji,List<Smjerovi> lSmjerovi)
        {
            //PRETRAGA STUDENTA

            bool b = false;

            foreach (var a in lStudents)
            {
                foreach (var s in lStudiji)
                {
                    foreach (var i in a.studij)
                    {
                        if (a.surname.ToUpper().Contains(search1.surname.ToUpper()) && i.id == s.id)
                        {
                            foreach (var ss in lSmjerovi) { 
                            Console.WriteLine("Ime i prezime: {0} {1}", a.name, a.surname);
                            Console.WriteLine("JMBAG: {0}", a.jmbag);
                            Console.WriteLine("Upisani studij: {0}", s.naziv_studija);
                            Console.WriteLine("Upisani smjer: {0}", ss.naziv_smjera);
                            Console.WriteLine("Godina: {0}", a.godina);
                            Console.WriteLine();
                            b = true;

                                break;
                        }
                    }
                    }

                }

            }

            if (b == false)
            {
                Console.WriteLine("Student nije pronaden");
                
            }
        
        }


        public static string GetPassword()
        {
            //SAKRIVANJE LOZINKE

            StringBuilder input = new StringBuilder();
            while (true)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return input.ToString();
        }

        public static void UserLog(List<Users> lUsers, List<Studiji> lStudiji, List<Students> lStudents, dynamic students_json, List<Smjerovi> lSmjerovi, List<PredmetS> lPredmeti)
        {

            //********* PRIJAVA KORISNIKA *********//

            string usern = "";
            string pass = "";
            Console.WriteLine();
            string textToEnterr = "VUV - REFERADA";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnterr.Length / 2)) + "}", textToEnterr));
            Console.WriteLine();
            Console.WriteLine();
            string textToEnter = "PRIJAVA U SUSTAV";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            Console.WriteLine();
            Console.Write("Username: ");
            usern = Convert.ToString(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Password: ");

            pass = GetPassword();

            Console.WriteLine();

            bool b = false;
            foreach (var u in lUsers)
            {
                if (u.username == usern && u.password == pass)
                {
                    if (u.type == "administrator")
                    {
                        MainMenu(u.username, lStudiji, lStudents, lUsers, students_json,u.type,lSmjerovi,lPredmeti);
                        b = true;
                        break;
                    }
                    if (u.type == "user") {
                        MainMenuUser(u.username, lStudiji, lStudents, lUsers, students_json, u.type, lSmjerovi, lPredmeti);

                        b = true;
                        break;
                    }
                }


            }
            if (b == false)
            {
                Console.WriteLine("Pogresno korisnicko ime ili lozinka");

            }
        }

        static public int AddStudij(List<Studiji> lStudiji) {

            //DODAVANJE STUDIJA

            try
            {
                dynamic studij = 0;
                int unos;
                do
                {

                    Console.Write("Studij: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - Menadzment (Informaticki menadzment)");
                    Console.WriteLine("2 - Menadzment (Menazment ruralnog turizma)");
                    Console.WriteLine("3 - Elektrotehnika (Telekomunikacije i informatika)");
                    Console.WriteLine("4 - Poduzetnistvo (Poduzetnistvo usluga)");
                    Console.WriteLine("5 - Racunarstvo (Programsko inzenjerstvo)");
                    Console.WriteLine();
                    Console.Write("Unos: ");

                    unos = Convert.ToInt32(Console.ReadLine());

                    if (unos < 0 || unos > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Pogresan unos.");
                    }
                } while (unos < 0 || unos > 5);
                Console.WriteLine();
                foreach (var s in lStudiji)
                {

                    if (s.id == unos)
                    {

                        studij = unos;
                        break;
                    }
                }

                return studij;
            }
            catch {

                Console.WriteLine("Pogreska");
                Environment.Exit(1);
                return 0;
            }
            }
           
        

        static public int AddSmjer(List<Smjerovi> lSmjerovi,List<Studiji> lStudiji,StudijS l) {

            //DODAVANJE SMJERA

            if (l.id == 1){
                int unos;
                do
                {
                    
                    Console.Write("Smjer: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - Informaticki menadzment (Menadzment)");
                    Console.WriteLine("2 - Menadzment ruralnog turizma (Menadzment)");
                    Console.WriteLine("3 - Telekomunikacije i informatika (Elektrotehnika)");
                    Console.WriteLine("4 - Poduzetnistvo usluga (Poduzetnistvo)");
                    Console.WriteLine("5 - Racunarstvo (Programsko inzenjerstvo)");
                    Console.WriteLine();
                    Console.Write("Unos: ");
                    unos = Convert.ToInt32(Console.ReadLine());

                    if (unos == 2 || unos == 3 || unos == 4 || unos == 5 || unos>5) {
                        Console.Clear();
                        Console.WriteLine("Nedostupan smjer za odabrani studij");
                    }
                    
                } while (unos == 2 || unos ==3 || unos==4 || unos==5 || unos > 5);
                return unos;
            }

            if (l.id == 2)
            {
                int unos;
                do
                {

                    Console.Write("Smjer: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - Informaticki menadzment (Menadzment)");
                    Console.WriteLine("2 - Menadzment ruralnog turizma (Menadzment)");
                    Console.WriteLine("3 - Telekomunikacije i informatika (Elektrotehnika)");
                    Console.WriteLine("4 - Poduzetnistvo usluga (Poduzetnistvo)");
                    Console.WriteLine("5 - Racunarstvo (Programsko inzenjerstvo)");
                    Console.WriteLine();
                    Console.Write("Unos: ");
                    unos = Convert.ToInt32(Console.ReadLine());
                    if (unos == 1 || unos == 3 || unos == 4 || unos == 5 || unos > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Nedostupan smjer za odabrani studij.");
                    }
                } while (unos == 1 || unos==3 || unos==4 || unos==5 || unos > 5);
                return unos;

            }

            if (l.id == 3)
            {
                int unos;
                do
                {
                    Console.Write("Smjer: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - Informaticki menadzment (Menadzment)");
                    Console.WriteLine("2 - Menadzment ruralnog turizma (Menadzment)");
                    Console.WriteLine("3 - Telekomunikacije i informatika (Elektrotehnika)");
                    Console.WriteLine("4 - Poduzetnistvo usluga (Poduzetnistvo)");
                    Console.WriteLine("5 - Racunarstvo (Programsko inzenjerstvo)");
                    Console.WriteLine();
                    Console.Write("Unos: ");
                    unos = Convert.ToInt32(Console.ReadLine());

                    if (unos ==1 || unos==2 || unos==4 || unos == 5 || unos > 5 )
                    {
                        Console.Clear();
                        Console.WriteLine("Nedostupan smjer za odabrani studij.");
                    }
                } while (unos == 1 || unos == 2 || unos == 4 || unos == 5 || unos > 5);

                return unos;
            }

            if (l.id == 4)
            {
                int unos;
                do
                {

                    Console.Write("Smjer: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - Informaticki menadzment (Menadzment)");
                    Console.WriteLine("2 - Menadzment ruralnog turizma (Menadzment)");
                    Console.WriteLine("3 - Telekomunikacije i informatika (Elektrotehnika)");
                    Console.WriteLine("4 - Poduzetnistvo usluga (Poduzetnistvo)");
                    Console.WriteLine("5 - Racunarstvo (Programsko inzenjerstvo)");
                    Console.WriteLine();
                    Console.Write("Unos: ");
                    unos = Convert.ToInt32(Console.ReadLine());
                    if (unos == 1 || unos == 2 || unos == 3 || unos == 5 || unos > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Nedostupan smjer za odabrani studij.");
                    }
                } while (unos == 1 || unos == 2 || unos == 3 || unos == 5 || unos > 5);

                return unos;
            }

            if (l.id == 5)
            {
                int unos;
                do
                {

                    Console.Write("Smjer: ");
                    Console.WriteLine();
                    Console.WriteLine("1 - Informaticki menadzment (Menadzment)");
                    Console.WriteLine("2 - Menadzment ruralnog turizma (Menadzment)");
                    Console.WriteLine("3 - Telekomunikacije i informatika (Elektrotehnika)");
                    Console.WriteLine("4 - Poduzetnistvo usluga (Poduzetnistvo)");
                    Console.WriteLine("5 - Programsko inzenjerstvo (Racunarstvo)");
                    Console.WriteLine();
                    Console.Write("Unos: ");
                    unos = Convert.ToInt32(Console.ReadLine());
                    if (unos == 1 || unos == 2 || unos == 3 || unos == 4  || unos > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Nedostupan smjer za odabrani studij.");
                    }
                } while (unos == 1 || unos == 2 || unos == 3 || unos == 4 || unos > 5);

                return unos;
            }

            return 0;

        }
        static public void AddStudent(List<Students> lStudents, dynamic json_studenti,List<Studiji> lStudiji,List<Smjerovi> lSmjerovi,List<PredmetS> lPredmeti)
        {
            //DODAVANJE STUDENATA
            
            int idd = lStudents.Count + 1;
            List<Students> nstudents = new List<Students>();

            List<StudijS> nstudiji = new List<StudijS>();

            List<SmjerS> nsmjerovi = new List<SmjerS>();

            
            string jmbag, surname, name,spol;
           
            
            int godina;
           
                Console.Write("Ime: ");
                name = Convert.ToString(Console.ReadLine());
                char n = char.ToUpper(name[0]);
                Console.Write("Prezime: ");
                surname = Convert.ToString(Console.ReadLine());
                char s = char.ToUpper(surname[0]);
                name.Substring(1).ToUpper();
                do
                {
                    Console.Write("JMBAG: ");
                    jmbag = Convert.ToString(Console.ReadLine());
                    if (jmbag.Length != 10)
                    {
                        Console.WriteLine("Neispravan JMBAG");
                    }

                } while (jmbag.Length != 10);

            int studijj = AddStudij(lStudiji);
            StudijS studij = new StudijS(studijj);

            nstudiji.Add(studij);


            int smjerr = AddSmjer(lSmjerovi,lStudiji,studij);
            SmjerS smjer= new SmjerS(smjerr);

            nsmjerovi.Add(smjer);

            Console.Write("Godina: ");
            godina = Convert.ToInt32(Console.ReadLine());

            Console.Write("Spol (m/z): ");
            spol = Convert.ToString(Console.ReadLine());
            Students student = new Students(idd, jmbag, s + surname.Substring(1), n + name.Substring(1), nstudiji, nsmjerovi, godina,spol);
                lStudents.Add(student);

                var json_data = JsonConvert.SerializeObject(lStudents, Formatting.Indented);
                File.WriteAllText(@"C:\Users\Sven\source\repos\KV\KV\studenti.json", json_data);
        }

        static void Main(string[] args)
        {

            //********* CITANJE JSON DATOTEKA *********//

            dynamic cfg_json = "";

            StreamReader sr = new StreamReader("C:\\Users\\Sven\\source\\repos\\KV\\KV\\config.json");
            using (sr)
            {
                cfg_json = sr.ReadToEnd();
            }
            List<Users> lUsers = new List<Users>();

            lUsers = JsonConvert.DeserializeObject<List<Users>>(cfg_json);

            sr.Close();

            dynamic studiji_json = "";

            StreamReader sr1 = new StreamReader("C:\\Users\\Sven\\source\\repos\\KV\\KV\\studiji.json");
            using (sr1)
            {
                studiji_json = sr1.ReadToEnd();
            }
            List<Studiji> lStudiji = new List<Studiji>();

            lStudiji = JsonConvert.DeserializeObject<List<Studiji>>(studiji_json);
            sr1.Close();

            dynamic students_json = "";

            StreamReader sr2 = new StreamReader("C:\\Users\\Sven\\source\\repos\\KV\\KV\\studenti.json");
            using (sr2)
            {
                students_json = sr2.ReadToEnd();
            }
            List<Students> lStudents = new List<Students>();

            lStudents = JsonConvert.DeserializeObject<List<Students>>(students_json);
            sr2.Close();

            dynamic smjerovi_json = "";

            StreamReader sr3 = new StreamReader("C:\\Users\\Sven\\source\\repos\\KV\\KV\\smjerovi.json");
            using (sr3)
            {
                smjerovi_json = sr3.ReadToEnd();
            }

            List<Smjerovi> lSmjerovi= new List<Smjerovi>();

            lSmjerovi = JsonConvert.DeserializeObject<List<Smjerovi>>(smjerovi_json);
            sr3.Close();

            dynamic predmeti_json = "";

            StreamReader sr4 = new StreamReader("C:\\Users\\Sven\\source\\repos\\KV\\KV\\smjerovi.json");
            using (sr4)
            {
                smjerovi_json = sr4.ReadToEnd();
            }

            List<PredmetS> lPredmeti = new List<PredmetS>();

            lPredmeti = JsonConvert.DeserializeObject<List<Smjerovi>>(predmeti_json);
            sr4.Close();

            UserLog(lUsers, lStudiji, lStudents, students_json,lSmjerovi,lPredmeti);


        }

    }
}
