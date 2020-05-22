﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public sealed class SingletonDBMS
    {
        //Istanza della classe
        private static SingletonDBMS istance = null;

        //Sintassi per la connessione al server SQl
        //per Windows Authentication
        //@Server =(Instance Server Name); Integrated Security=True;
        //per SQL server Authentication
        //@Server =(Instance Server Name); Integrated Security=True; User ID=(user name); Password=(password);
        //private static SqlConnection myConnection = new SqlConnection("Server=DESKTOP-V6TJPP0\\SQLEXPRESS;" + "Integrated Security=True");
        private static SqlConnection myConnection = new SqlConnection("Server=DESKTOP-V6TJPP0\\SQLEXPRESS;" + "Integrated Security=True");

        private SingletonDBMS()
        {
        }

        public static SingletonDBMS GetInstance()
        {
            if (istance == null)
            {
                istance = new SingletonDBMS();
            }
            return istance;
        }

        /// <summary>
        /// Creazione del DB
        /// </summary>
        public void CreateDb()
        {
            //Query per la creazione del DB
            string sqlCreateDb = "USE MASTER " +
                                 "DROP DATABASE IF EXISTS db_big_scuola; " +
                                 "CREATE DATABASE db_big_scuola; ";

            //Query per l'utilizzo del DB e per la creazione delle tabelle
            string sqlCreateTables = "USE db_big_scuola; \n" +
                                    "CREATE TABLE games( " +
                                        "id                     INT             IDENTITY(0,1)   NOT NULL, " +
                                        "name                   VARCHAR(100)    NOT NULL, " +
                                        "single_league          INT             NULL, " +
                                        "game_in_the_ranking    INT             NULL, " +
                                        "PRIMARY KEY (id), " +
                                        "UNIQUE (name)" +
                                    " );\n" +

                                    "CREATE TABLE tournaments( " +
                                        "id             INT             IDENTITY(0,1)   NOT NULL, " +
                                        "id_game        INT             NOT NULL, " +
                                        "date_hour      DATETIME        NOT NULL, " +
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
                                        "id             INT             IDENTITY(0,1)   NOT NULL, " +
                                        "id_tournament  INT             NOT NULL, " +
                                        "progressive_n  INT             NULL, " +
                                        "players_n      INT             NOT NULL, " +
                                        "turn           INT             NOT NULL, " +
                                        "PRIMARY KEY (id), " +
                                        "CONSTRAINT [FK_tables_tournaments] FOREIGN KEY ([id_tournament]) REFERENCES [dbo].[tournaments] ([id])" +
                                    " );\n" +

                                    "CREATE TABLE players( " +
                                        "id             INT             IDENTITY(0,1)   NOT NULL, " +
                                        "nickname       VARCHAR(100)    NOT NULL, " +
                                        "firstname      VARCHAR(200)    NOT NULL, " +
                                        "lastname       VARCHAR(200)    NOT NULL, " +
                                        "email          VARCHAR(200)    NOT NULL, " +
                                        "phone          VARCHAR(200)    NOT NULL, " +
                                        "birthdate      DATE            NOT NULL, " +
                                        "elo            FLOAT           NULL, " + //se si iscrive un nuovo giocatore non ha l'elo 
                                        "PRIMARY KEY(id), " +
                                        "UNIQUE (nickname), " +
                                        "UNIQUE (email)" +
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
                                        "points         INT           NULL," +
                                        "PRIMARY KEY (id_table, id_player), " +
                                        "CONSTRAINT[FK_compositions_players] FOREIGN KEY([id_player]) REFERENCES[dbo].[players]([id]), " +
                                        "CONSTRAINT[FK_compositions_tables] FOREIGN KEY([id_table]) REFERENCES[dbo].[tables]([id])" +
                                   ");";

            //Query per l'inserimenti di 4 giochi
            String sqlInsertGames = "INSERT INTO games(name) VALUES('Trivial Puresuit');" +
                                    "INSERT INTO games(name) VALUES('Cluedo');" +
                                    "INSERT INTO games(name) VALUES('Scarabeo');" +
                                    "INSERT INTO games(name) VALUES('Risiko');";

            //Query per l'inserimenti di 9 giocatori
            String sqlInsertPlayers = "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g1', 'Sara', 'Rossi', 'fat@jolly.it', '+39 111 213546', '2000-02-04');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g2', 'Fede','Mai', 'tigro@jolly.it', '+39 222 213546', '2001-04-23');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g3', 'Leo', 'Joseph', 'ciao@jolly.it', '+39 131 213546', '1991-06-25');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g4', 'Nerd', 'Boi', 'nerd@jolly.it', '+39 211 213546', '1962-12-31');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g5', 'Paul','Grim', 'a@jolly.it', '+39 322 213546', '1981-04-23');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g6', 'Lin Kun', 'Fu', 'lao@jolly.it', '+39 139 213546', '1964-06-25');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g7', 'Ciao', 'Bye', 'bye@jolly.it', '+39 211 213546', '1962-12-31');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g8', 'Lock', 'Down', 'key@jolly.it', '+39 149 213546', '1977-06-25');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('g9', 'Uomo', 'Verde', 'green@jolly.it', '+39 211 213546', '1962-12-31');";
            SqlCommand command;

            try {
                myConnection.Open();
                command = new SqlCommand(sqlCreateDb, myConnection);
                command.ExecuteNonQuery();
                command = new SqlCommand(sqlCreateTables, myConnection);
                command.ExecuteNonQuery();
                command = new SqlCommand(sqlInsertGames, myConnection);
                command.ExecuteNonQuery();
                command = new SqlCommand(sqlInsertPlayers, myConnection);
                command.ExecuteNonQuery();
            } catch(Exception e) { }
        }

        /// <summary>
        /// Restituisce l'id del gioco dato il nome
        /// </summary>
        /// <param name="name"> nome del gioco </param>
        /// <returns> restituisce l'id del gioco </returns>
        private int GetGameID(String name)
        {
            int id = -1;
            //Query per ottenere l'id del gioco
            String sqlQuery = "SELECT id FROM db_big_scuola.dbo.games " +
                              "WHERE name =@n;";
            SqlCommand command = new SqlCommand(sqlQuery, myConnection);
            command.Parameters.AddWithValue("@n", name);
            try
            {

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                reader.Close();
                
            }
            catch (Exception e) { }

            return id;
        }

        /// <summary>
        /// Restituisce l'id del giocatore dato il nickname
        /// </summary>
        /// <param name="nickname"> nickname </param>
        /// <returns></returns>
        private int GetPlayerID(String nickname)
        {
            int id = -1;
            String sqlGetIdP = "SELECT id FROM db_big_scuola.dbo.players " +
                                "WHERE nickname = @n";
            SqlCommand command = new SqlCommand(sqlGetIdP, myConnection);
            command.Parameters.AddWithValue("@n", nickname);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            return id;
        }

        /// <summary>
        /// Restituisce l'id del torneo
        /// </summary>
        /// <param name="tournamentName"> nome del torneo </param>
        /// <returns>id torneo</returns>
        private int GetTournamentID(String tournamentName)
        {
            int id = -1;
            String sqlGetIdT = "SELECT id FROM db_big_scuola.dbo.tournaments " +
                                "WHERE name = @t;";
            SqlCommand command = new SqlCommand(sqlGetIdT, myConnection);
            command.Parameters.AddWithValue("@t", tournamentName);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            return id;

        }

        /// <summary>
        /// Restituisce l'id del tavolo
        /// </summary>
        /// <param name="rn"> numero del turno </param>
        /// <param name="tn"> numero del tavolo </param>
        /// <returns> restisuice un determinato tavolo di un determinato turno </returns>
        private int GetTableID(int rn, int tn)
        {
            int id = -1;
            String sqlGetIdT = "SELECT id FROM db_big_scuola.dbo.tables " +
                                "WHERE turn = @rn AND progressive_n = @tn;";

            SqlCommand command = new SqlCommand(sqlGetIdT, myConnection);
            command.Parameters.AddWithValue("@rn", rn);
            command.Parameters.AddWithValue("@tn", tn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            return id;
        }

        /// <summary>
        /// Inserimento del torneo nel DB
        /// </summary>
        /// <param name="t"> istanza di torneo </param>
        /// <returns> restisuice un boolean che specifica se l'esequzione della funziona è avvenuta con sucesso</returns>
        public bool InsertTournament(Tournament t)
        {
            SqlCommand command;
            bool success = true;

            //Query per l'inserimento del torneo all'interno del DB
            String sqlCreateT = "INSERT INTO db_big_scuola.dbo.tournaments(id_game, date_hour, name, players_n, address) " +
                                    "VALUES( @IDgame, @date, @nameT, @players_n, @address);";
            try
            {
                command = new SqlCommand(sqlCreateT, myConnection);
                command.Parameters.AddWithValue("@IDgame", GetGameID(t.Game));
                command.Parameters.AddWithValue("@date", t.Date);
                command.Parameters.AddWithValue("@nameT", t.Name);
                command.Parameters.AddWithValue("@players_n", t.getListPlayers().Count);
                command.Parameters.AddWithValue("@address", t.Address);
                //Esequzione delle query
                command.ExecuteNonQuery();
                
                InsertParticipations(t);

            } catch (Exception e)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Inserisce le associazioni tra gioctore e torneo
        /// </summary>
        /// <param name="t"> istanza di torno </param>
        private void InsertParticipations(Tournament t)
        {
            int idT = GetTournamentID(t.Name);
            String nickname;
            int idP;
            SqlCommand command;
            String sqlParticipation = "INSERT INTO db_big_scuola.dbo.participations " +
                                        "VALUES( @idT, @idP);";

            
            //Inserimento dati nella tabella participations
            for (int i = 0; i < t.getListPlayers().Count; i++)
            {
                nickname = t.getListPlayers().ElementAt(i).Nickname;
                idP = GetPlayerID(nickname);
                command = new SqlCommand(sqlParticipation, myConnection);
                command.Parameters.AddWithValue("@idT", idT);
                command.Parameters.AddWithValue("@idP", idP);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserisce i dati iniziali del turno
        /// </summary>
        /// <param name="t"> istanza di torneo </param>
        /// <returns> restituisce una conferma dell'esecuzione </returns>
        public bool InsertTurn(Tournament t)
        {
            bool success = true;
            int idTable;
            int prog_n;
            int idP;
            String playerNickname;

            int idT = GetTournamentID(t.Name);
            int players_n = t.CurrentTurn.TablePlayersNumber;

            try
            {
                for (int i = 0; i < t.CurrentTurn.getListTables().Count; i++)
                {
                    String sqlInsertTable = sqlInsertTable = "INSERT INTO db_big_scuola.dbo.tables(id_tournament, players_n, turn, progressive_n) " +
                                            "VALUES( @idT, @players_n, @rn, @prog_n);";
                    SqlCommand command1 = new SqlCommand(sqlInsertTable, myConnection);
                    //prog_n rappresenta il numero del tavolo del turno corrente
                    prog_n = t.CurrentTurn.getListTables().ElementAt(i).TableNumber;
                    //set parametri query 1
                    command1.Parameters.AddWithValue("@idT", idT);
                    command1.Parameters.AddWithValue("@players_n", players_n);
                    command1.Parameters.AddWithValue("@rn", t.RoundNumber);
                    command1.Parameters.AddWithValue("@prog_n", prog_n);
                    command1.ExecuteNonQuery();
                    idTable = GetTableID(t.RoundNumber, prog_n);
                    for (int k = 0; k < players_n; k++)
                    {
                        playerNickname = t.CurrentTurn.getListTables().ElementAt(i).getPlayers().ElementAt(k).Nickname;
                        idP = GetPlayerID(playerNickname);
                        String sqlInsertComposition = "INSERT INTO db_big_scuola.dbo.compositions(id_table, id_player) " +
                                             "VALUES( @idTable, @idP);";
                        SqlCommand command2 = new SqlCommand(sqlInsertComposition, myConnection);
                        //set parametri query 2
                        command2.Parameters.AddWithValue("@idTable", idTable);
                        command2.Parameters.AddWithValue("@idP", idP);
                        command2.ExecuteNonQuery();
                    }
                }

            } catch(Exception e)
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Aggiorna il punteggio del giocatore
        /// </summary>
        /// <param name="t"> istanza di toeno </param>
        /// <returns> restituisce una conferma dell'esecuzione </returns>
        public bool InsertScores(Tournament t)
        {
            bool success = true;

            int idTable;
            int idPlayer;

            try
            {
                for (int i = 0; i < t.CurrentTurn.getListTables().Count; i++)
                {
                    idTable = GetTableID(t.RoundNumber, t.CurrentTurn.getListTables().ElementAt(i).TableNumber);
                    for (int k = 0; k < t.CurrentTurn.getListTables().ElementAt(i).getPlayers().Count; k++)
                    {
                        String sqlUpdateComposition = "UPDATE db_big_scuola.dbo.compositions " +
                                          "SET points = @points " +
                                          "WHERE id_table = @idTable AND id_player = @idPlayer";
                        SqlCommand command = new SqlCommand(sqlUpdateComposition, myConnection);
                        String nickname = t.CurrentTurn.getListTables().ElementAt(i).getPlayers().ElementAt(k).Nickname;
                        int points = t.CurrentTurn.getListTables().ElementAt(i).getPlayers().ElementAt(k).Points;
                        idPlayer = GetPlayerID(nickname);
                        command.Parameters.AddWithValue("@idTable", idTable);
                        command.Parameters.AddWithValue("@idPlayer", idPlayer);
                        command.Parameters.AddWithValue("@points", points);
                        command.ExecuteNonQuery();
                    }
                }

            } catch (Exception e)
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Restituisce una lista di giochi
        /// </summary>
        /// <returns> linkedlist </returns>
        public LinkedList<string> GetGamesFromDB()
        {
            LinkedList<string> games = new LinkedList<string>();
            String sqlSelectGames = "SELECT name " +
                                    "FROM db_big_scuola.dbo.games";
            SqlCommand command = new SqlCommand(sqlSelectGames, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    games.AddLast(reader.GetString(0));
                }
            }
            return games;
        }

        /// <summary>
        /// restituisce la lista dei giocatori
        /// </summary>
        /// <returns> linkedlist </returns>
        public LinkedList<string> GetPlayersFromDB()
        {
            LinkedList<string> players= new LinkedList<string>();
            String sqlSelectPlayers = "SELECT nickname " +
                                      "FROM db_big_scuola.dbo.players";
            SqlCommand command = new SqlCommand(sqlSelectPlayers, myConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    players.AddLast(reader.GetString(0));
                }
            }
            return players;
        }
    }
}
