using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_big_scuola
{
    public sealed class SingletoneDBMS
    {
        //Istanza della classe
        private static SingletoneDBMS istance = null;

        private static SqlConnection myConnection = new SqlConnection("Server=DESKTOP-V6TJPP0\\SQLEXPRESS;" + "Integrated Security=True");

        private SingletoneDBMS()
        {
            //Sintassi per la connessione al server SQl
            //per Windows Authentication
            //@Server =(Instance Server Name); Integrated Security=True;
            //per SQL server Authentication
            //@Server =(Instance Server Name); Integrated Security=True; User ID=(user name); Password=(password);
            //myConnection = new SqlConnection("Server=DESKTOP-V6TJPP0\\SQLEXPRESS;" + "Integrated Security=True");
        }

        public static SingletoneDBMS GetInstance()
        {
            if (istance == null)
            {
                istance = new SingletoneDBMS();
            }
            return istance;
        }

        public void createDb()
        {
            string sqlCreateDb = "IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'db_big_scuola')" +
                                        "BEGIN " +
                                            "CREATE DATABASE[db_big_scuola] " +
                                        "END " +
                                    "ELSE " +
                                        "BEGIN " +
                                            "DROP DATABASE [db_big_scuola] " +
                                            "CREATE DATABASE [db_big_scuola] " +
                                        "END;\n";
            string sqlCreateTables = "USE db_big_scuola; \n" +
                                    "CREATE TABLE games( " +
                                        "id                     INT             NOT NULL    IDENTITY, " +
                                        "name                   VARCHAR(100)    NOT NULL, " +
                                        "single_league          INT             NULL, " +
                                        "game_in_the_ranking    INT             NOT NULL, " +
                                        "PRIMARY KEY (id), " +
                                        "UNIQUE (name)" +
                                    " );\n" +

                                    "CREATE TABLE tournaments( " +
                                        "id             INT             NOT NULL    IDENTITY, " +
                                        "id_game        INT             NOT NULL, " +
                                        "data_hour      DATETIME        NOT NULL, " +
                                        "name           VARCHAR(100)    NOT NULL, " +
                                        "online         SMALLINT        NULL, " +
                                        "players_n      INT             NOT NULL, " +
                                        "description    TEXT            NULL, " +
                                        "progressive    INT             NULL, " +
                                        "final          BIT             NOT NULL DEFAULT 0, " +
                                        "address        VARCHAR(205)    NOT NULL, " +
                                        "PRIMARY KEY (id), " +
                                        "CONSTRAINT [FK_tournaments_games] FOREIGN KEY ([id_game]) REFERENCES [dbo].[games] ([id])" +
                                    " );\n" +

                                    "CREATE TABLE tables( " +
                                        "id             INT             NOT NULL    IDENTITY, " +
                                        "id_tournament  INT             NOT NULL, " +
                                        "progressive_n  INT             NULL, " +
                                        "players_n      INT             NOT NULL, " +
                                        "turn           INT             NOT NULL, " +
                                        "PRIMARY KEY (id), " +
                                        "CONSTRAINT [FK_tables_tournaments] FOREIGN KEY ([id_tournament]) REFERENCES [dbo].[tournaments] ([id])" +
                                    " );\n" +

                                    "CREATE TABLE players( " +
                                        "id             INT             NOT NULL    IDENTITY, " +
                                        "nickname       VARCHAR(100)    NOT NULL, " +
                                        "firstname      VARCHAR(200)    NOT NULL, " +
                                        "lastname       VARCHAR(200)    NOT NULL, " +
                                        "email          VARCHAR(200)    NOT NULL, " +
                                        "phone          VARCHAR(200)    NOT NULL, " +
                                        "birthdate      DATE            NOT NULL, " +
                                        "elo            FLOAT           NULL, " + //se si iscrive un nuovo giocatore non ha l'elo 
                                        "PRIMARY KEY(id), " +
                                        "UNIQUE (nickname)" +
                                    " );\n" +

                                    "CREATE TABLE participations( " +
                                        "id_tournament  INT             NOT NULL, " +
                                        "id_player      INT             NOT NULL, " +
                                        "PRIMARY KEY(id_tournament, id_player), " +
                                        "CONSTRAINT [FK_participations_players] FOREIGN KEY ([id_player]) REFERENCES [dbo].[players] ([id])," +
                                        "CONSTRAINT [FK_participations_tournaments] FOREIGN KEY ([id_tournament]) REFERENCES [dbo].[tournaments] ([id])" +

                                    " );\n" +

                                    "CREATE TABLE compositions( " +
                                        "id_table       INT             NOT NULL, " +
                                        "id_player      INT             NOT NULL, " +
                                        "punteggio      FLOAT           NULL" +
                                        "PRIMARY KEY (id_table, id_player), " +
                                        "CONSTRAINT[FK_compositions_players] FOREIGN KEY([id_player]) REFERENCES[dbo].[players]([id]), " +
                                        "CONSTRAINT[FK_compositions_tables] FOREIGN KEY([id_table]) REFERENCES[dbo].[tables]([id])" +
                                   ");";
            SqlCommand command;

            myConnection.Open();
            command = new SqlCommand(sqlCreateDb, myConnection);
            command.ExecuteNonQuery();
            command = new SqlCommand(sqlCreateTables, myConnection);
            command.ExecuteNonQuery();
            myConnection.Close();
        }

        public bool CreateNewTournament(bigTournamentManager.Tournament t)
        {
            bool success = false;
            String sqlQuery = "INSERT INTO tournaments(id_game, data_hour, name, players_n, address) " +
                              "VALUES (" + GetGame(t.Game) + ", \"" + GetDataHourString(t.Date) + "\", \"" + t.Name + "\"," + t.getListPlayers().Count + " \"" + t.Address + " )";



            return success;
        }

        private String GetDataHourString(DateTime date)
        {
            String str = " ";
            return str;
        }

        private int GetGame(String nameGame)
        {
            int id = 0;



            return id;
        }

    }
}
