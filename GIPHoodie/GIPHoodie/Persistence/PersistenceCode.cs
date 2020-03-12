using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GIPHoodie.Models;
using MySql.Data.MySqlClient;

namespace GIPHoodie.Persistence
{
    public class PersistenceCode
    {
        string connStr = "server=localhost;user id=root;password=Test123;database=dbwebshop";

        public List<Artikel> loadArtikels() //laden van de verschillende artikels in de webshop
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string qry = "select * from tblartikel";
            MySqlCommand cmd = new MySqlCommand(qry, conn);
            MySqlDataReader dtr = cmd.ExecuteReader();
            List<Artikel> lijst = new List<Artikel>();
            while (dtr.Read())
            {
                Artikel artikel = new Artikel();
                artikel.ArtikelID = Convert.ToInt32(dtr["artikelid"]);
                artikel.Naam = Convert.ToString(dtr["naam"]);
                artikel.Voorraad = Convert.ToInt32(dtr["voorraad"]);
                artikel.Prijs = Convert.ToDouble(dtr["prijs"]);
                artikel.Foto = Convert.ToString(dtr["foto"]);
                lijst.Add(artikel);
            }
            conn.Close();
            return lijst;
        }


        public Artikel loadArtikel(int ArtID) //laden van het geselecteerde artikel om deze in de winkelmand toe te voegen
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string qry = "select * from tblartikel where artikelid=" + ArtID;
            MySqlCommand cmd = new MySqlCommand(qry, conn);
            MySqlDataReader dtr = cmd.ExecuteReader();
            Artikel artikel = new Artikel();
            while (dtr.Read())
            {
                artikel.ArtikelID = Convert.ToInt32(dtr["artikelid"]);
                artikel.Naam = Convert.ToString(dtr["naam"]);
                artikel.Voorraad = Convert.ToInt32(dtr["voorraad"]);
                artikel.Prijs = Convert.ToDouble(dtr["prijs"]);
                artikel.Foto = Convert.ToString(dtr["foto"]);
            }
            conn.Close();
            return artikel;
        }

        public void PasMandAan(WinkelmandItem winkelmanditem) // een geselecteerd artikel in de database in een winkelmand opslaan of aanpassen
        {
            
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string qry1 = "select * from tblartikel where KlantID=" + winkelmanditem.KlantNr + " and ArtikelID="
                + winkelmanditem.ArtikelNr;
            MySqlCommand cmd = new MySqlCommand(qry1, conn);
            MySqlDataReader dtr = cmd.ExecuteReader();
            bool mand = true;
            if(dtr.HasRows)
            {
               mand = true;
            }
            else
            {
                mand = false;

            }
            conn.Close();

            conn.Open();
            if (mand==true)
            {
                //int aantal = winkelmanditem.Aantal;
                string qry2 = "update tblwinkelmand SET Aantal = '" + winkelmanditem.Aantal + "' where(KlantID = '1') and(ArtikelID = '1')";
            }
            else
            {
                string qry3 = "insert into tblwinkelmand(KlantID, ArtikelID, Aantal) values(" + winkelmanditem.KlantNr +
                 "," + winkelmanditem.ArtikelNr + "," + winkelmanditem.Aantal + ")";
            }
            conn.Close();
            conn.Open();
            string qry4 = "update tblartikel set voorraad = vooraad - aantal where ArtikelID=" + winkelmanditem.ArtikelNr;
            conn.Close();
        }
    }
}
