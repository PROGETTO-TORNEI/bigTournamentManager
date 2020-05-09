using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bigTournamentManager
{
    public sealed class SingletoneDBMS
    {
        //Istanza della classe
        private static SingletoneDBMS istance = null;

        //Sintassi per la connessione al server SQl
        //per Windows Authentication
        //@Server =(Instance Server Name); Integrated Security=True;
        //per SQL server Authentication
        //@Server =(Instance Server Name); Integrated Security=True; User ID=(user name); Password=(password);
        //myConnection = new SqlConnection("Server=DESKTOP-V6TJPP0\\SQLEXPRESS;" + "Integrated Security=True");
        private static SqlConnection myConnection = new SqlConnection("Server=DESKTOP-JJMRU4D\\SQLEXPRESS;" + "Integrated Security=True");

        private SingletoneDBMS()
        {
        }

        public static SingletoneDBMS GetInstance()
        {
            if (istance == null)
            {
                istance = new SingletoneDBMS();
            }
            return istance;
        }

        /// <summary>
        /// Creazione del DB
        /// </summary>
        public void createDb()
        {
            //Query per la creazione del DB
            string sqlCreateDb = "IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'db_big_scuola')" +
                                        "BEGIN " +
                                            "CREATE DATABASE[db_big_scuola] " +
                                        "END " +
                                    "ELSE " +
                                        "BEGIN " +
                                            "DROP DATABASE [db_big_scuola] " +
                                            "CREATE DATABASE [db_big_scuola] " +
                                        "END;\n";

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
                                        "VALUES('fattrice', 'Sara', 'Rossi', 'fat@jolly.it', '+39 111 213546', '2000-02-04');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('destroyer25', 'Fede','Mai', 'tigro@jolly.it', '+39 222 213546', '2001-04-23');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('BassiMaestro', 'Leo', 'Joseph', 'ciao@jolly.it', '+39 131 213546', '1991-06-25');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('nerdBoy', 'Nerd', 'Boi', 'nerd@jolly.it', '+39 211 213546', '1962-12-31');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('boh', 'Paul','Grim', 'a@jolly.it', '+39 322 213546', '1981-04-23');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('kimJong', 'Lin Kun', 'Fu', 'lao@jolly.it', '+39 139 213546', '1964-06-25');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('hello', 'Ciao', 'Bye', 'bye@jolly.it', '+39 211 213546', '1962-12-31');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('chiuso', 'Lock', 'Down', 'key@jolly.it', '+39 149 213546', '1977-06-25');" +
                                      "INSERT INTO players(nickname, firstname, lastname, email, phone, birthdate)" +
                                        "VALUES('hulk', 'Uomo', 'Verde', 'green@jolly.it', '+39 211 213546', '1962-12-31');";
            SqlCommand command;

            myConnection.Open();
            command = new SqlCommand(sqlCreateDb, myConnection);
            command.ExecuteNonQuery();
            command = new SqlCommand(sqlCreateTables, myConnection);
            command.ExecuteNonQuery();
            command = new SqlCommand(sqlInsertGames, myConnection);
            command.ExecuteNonQuery();
            command = new SqlCommand(sqlInsertPlayers, myConnection);
            command.ExecuteNonQuery();
            myConnection.Close();
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
            String sqlQuery = "SELECT id FROM games " +
                              "WHERE name = @n;";
            SqlCommand command = new SqlCommand(sqlQuery, myConnection);

            myConnection.Open();
            command.Parameters.AddWithValue("@n", name);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            myConnection.Close();

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
            String sqlGetIdP = "SELECT id FROM players" +
                                "WHERE nickname = @n";
            SqlCommand command = new SqlCommand(sqlGetIdP, myConnection);
            myConnection.Open();
            command.Parameters.AddWithValue("@n", nickname);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            myConnection.Close();
            return id;
        }

        /// <summary>
        /// Restituisce l'id del torneo
        /// </summary>
        /// <param name="tournamentName"> nome del torneo </param>
        /// <returns></returns>
        private int GetTournamentID(String tournamentName)
        {
            int id = -1;
            String sqlGetIdT = "SELECT id FROM tournaments " +
                                "WHERE name = @t;";
            SqlCommand command = new SqlCommand(sqlGetIdT, myConnection);
            myConnection.Open();
            command.Parameters.AddWithValue("@t", tournamentName);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            myConnection.Close();

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
            String sqlGetIdT = "SELECT id FROM tournaments " +
                                "WHERE turn = = @rn AND progressive_n = @tn;";

            SqlCommand command = new SqlCommand(sqlGetIdT, myConnection);
            myConnection.Open();
            command.Parameters.AddWithValue("@rn", rn);
            command.Parameters.AddWithValue("@tn", tn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            myConnection.Close();

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
            String sqlCreateT = "INSERT INTO tournaments(id_game, date_hour, name, players_n, address) " +
                                    "VALUES( @IDgame, @date, @nameT, @players_n, @address);";
            try
            {
                //Esequzione delle query
                myConnection.Open();
                command = new SqlCommand(sqlCreateT, myConnection);
                command.Parameters.AddWithValue("@IDgame", GetGameID(t.Name));
                command.Parameters.AddWithValue("@date", t.Date.ToString("YYYY'-'MM'-'dd' 'HH':'mm"));
                command.Parameters.AddWithValue("@nameT", t.Name);
                command.Parameters.AddWithValue("@players_n", t.getListPlayers().Count);
                command.Parameters.AddWithValue("@address", t.Address);
                
                command.ExecuteNonQuery();
                myConnection.Close();

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
        /// <param name="c"> istanza della connessione al DB </param>
        private void InsertParticipations(Tournament t)
        {
            int idT = GetTournamentID(t.Name);
            String nickname;
            int idP;
            SqlCommand command;
            String sqlParticipation = "INSERT INTO participations " +
                                        "VALUES( @idT, @idP);";

            myConnection.Open();
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
            myConnection.Close();
        }

        /// <summary>
        /// Inserisce i dati iniziali del turno
        /// </summary>
        /// <param name="t"> istanza di torneo </param>
        /// <param name="rn"> numero del turno (roundNumber) </param>
        /// <returns> restituisce una conferma dell'esecuzione </returns>
        public bool InsertTurn(Tournament t, int rn)
        {
            bool success = true;

            int idTable;
            int prog_n;
            int idP;
            int idT = GetTournamentID(t.Name);
            int players_n = t.CurrentTurn.TablePlayersNumber;
            String sqlInsertTable = sqlInsertTable = "INSERT INTO tables(id_tournament, players_n, turn, progressive_n) " +
                                                        "VALUES( @idT, @players_n, @rn, @prog_n);";
            String sqlInsertComposition = "INSERT INTO compositions(id_table, id_player) " +
                                             "VALUES( @idTable, @idP);";

            String playerNickname;
            SqlCommand command1 = new SqlCommand(sqlInsertTable, myConnection);
            SqlCommand command2 = new SqlCommand(sqlInsertComposition, myConnection);
            try
            {

                myConnection.Open();
                for (int i = 0; i < t.CurrentTurn.getListTables().Count; i++)
                {
                    //prog_n rappresenta il numero del tavolo del turno corrente
                    prog_n = t.CurrentTurn.getListTables().ElementAt(i).TableNumber;
                    //set parametri query 1
                    command1.Parameters.AddWithValue("@idT", idT);
                    command1.Parameters.AddWithValue("@players_n", players_n);
                    command1.Parameters.AddWithValue("@rn", rn);
                    command1.Parameters.AddWithValue("@prog_n", prog_n);
                    command1.ExecuteNonQuery();

                    idTable = GetTableID(rn, prog_n);
                    for (int k = 0; k < players_n; k++)
                    {
                        playerNickname = t.CurrentTurn.getListTables().ElementAt(i).getPlayers().ElementAt(k).Nickname;
                        idP = GetPlayerID(playerNickname);
                        //set parametri query 2
                        command2.Parameters.AddWithValue("@idTable", idTable);
                        command2.Parameters.AddWithValue("@idP", idP);
                        command2.ExecuteNonQuery();
                    }
                }
                myConnection.Close();

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
        /// <param name="rn"> numero del turno (roundNumber) </param>
        /// <returns> restituisce una conferma dell'esecuzione </returns>
        public bool InsertPointTurn(Tournament t, int rn)
        {
            bool success = true;

            int idTable;
            int idPlayer;

            String sqlUpdateComposition = "UPDATE compositions " +
                                          "SET points = @points " +
                                          "WHERE id_table = @idTable AND @id_player = @idPlayer";
            SqlCommand command = new SqlCommand(sqlUpdateComposition, myConnection);

            try
            {
                myConnection.Open();
                for (int i = 0; i < t.CurrentTurn.getListTables().Count; i++)
                {
                    idTable = GetTableID(rn, t.CurrentTurn.getListTables().ElementAt(i).TableNumber);
                    for (int k = 0; k < t.CurrentTurn.getListTables().ElementAt(i).getPlayers().Count; k++)
                    {
                        idPlayer = GetPlayerID(t.CurrentTurn.getListTables().ElementAt(i).getPlayers().ElementAt(k).Nickname);
                        command.Parameters.AddWithValue("@idTable", idTable);
                        command.Parameters.AddWithValue("@idPlayer", idPlayer);
                        command.ExecuteNonQuery();
                    }
                }
                myConnection.Close();

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
                                    "FROM games";
            SqlCommand command = new SqlCommand(sqlSelectGames, myConnection);
            myConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    games.AddLast(reader.GetString(0));
                }
            }
            myConnection.Close();

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
                                      "FROM players";
            SqlCommand command = new SqlCommand(sqlSelectPlayers, myConnection);
            myConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    players.AddLast(reader.GetString(0));
                }
            }
            myConnection.Close();

            return players;
        }
    }
}
