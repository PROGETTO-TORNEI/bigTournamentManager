using System;
using System.Collections.Generic;
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
        private static SqlConnection myConnection = new SqlConnection("Server=DESKTOP-V6TJPP0\\SQLEXPRESS;" + "Integrated Security=True");

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
                                        "punteggio      FLOAT           NULL," +
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
        /// Inserimento del torneo nel DB
        /// </summary>
        /// <param name="t"> istanza di torneo </param>
        /// <returns> restisuice un boolean che specifica se l'esequzione della funziona è avvenuta con sucesso</returns>
        public bool InsertTournament(Tournament t)
        {
            SqlCommand command;
            bool success = true;


            //Query per l'inserimento del torneo all'interno del DB
            String sqlCreateT = "INSERT INTO tournaments(id_game, data_hour, name, players_n, address) " +
                                    "VALUES( " + 
                                            GetGameID(t.Game) + "," +
                                            " '" + t.Date.ToString("YYYY'-'MM'-'dd' 'HH':'mm") + "'," + 
                                            " '" + t.Name + "'," + 
                                            t.getListPlayers().Count + "," + 
                                            " '" + t.Address + "' );";            
            try
            {
                //Esequzione delle query
                myConnection.Open();
                command = new SqlCommand(sqlCreateT, myConnection);
                command.ExecuteNonQuery();
                InsertParticipations(t, myConnection);
                myConnection.Close();
            } catch (Exception e)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Restituisce l'id del gioco dato il nome
        /// </summary>
        /// <param name="name"> nome del gioco </param>
        /// <returns> restituisce l'id del gioco </returns>
        private int GetGameID(String name)
        {
            int id;
            //Query per ottenere l'id del gioco
            String sqlQuery = "SELECT id FROM games " +
                              "WHERE name = '" + name + "'";
            SqlCommand command;

            myConnection.Open();
            command = new SqlCommand(sqlQuery, myConnection);
            id = command.ExecuteNonQuery();
            myConnection.Close();

            return id;
        }

        //Restituisce l'id del giocatore dato il nickname
        private int GetPlayerID(String nickname)
        {
            int id;
            String sqlGetIdP = "SELECT id FROM players" +
                                "WHERE nickname = '" + nickname + "'";
            myConnection.Open();
            SqlCommand command = new SqlCommand(sqlGetIdP, myConnection);
            id = command.ExecuteNonQuery();
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
            int id;
            SqlCommand command;
            String sqlGetIdT = "SELECT id FROM tournaments " +
                                "WHERE name = '" + tournamentName + "'";
            myConnection.Open();
            command = new SqlCommand(sqlGetIdT, myConnection);
            id = command.ExecuteNonQuery();
            myConnection.Close();

            return id;

        }

        /// <summary>
        /// Restituisce l'id del tavolo
        /// </summary>
        /// <param name="rn"> numero del turno </param>
        /// <returns> restisuice il tavolo di un determinato turno </returns>
        private int GetTableID(int rn)
        {
            int id;
            SqlCommand command;
            String sqlGetIdT = "SELECT id FROM tournaments " +
                                "WHERE turn = " + rn + ""
;
            myConnection.Open();
            command = new SqlCommand(sqlGetIdT, myConnection);
            id = command.ExecuteNonQuery();
            myConnection.Close();

            return id;
        }
        
        /// <summary>
        /// Inserisce le associazioni tra gioctore e torneo
        /// </summary>
        /// <param name="t"> istanza di torno </param>
        /// <param name="c"> istanza della connessione al DB </param>
        private void InsertParticipations(Tournament t, SqlConnection c)
        {
            int idT = GetTournamentID(t.Name);
            SqlCommand command;

            //Inserimento dati nella tabella participations
            for (int i = 0; i < t.getListPlayers().Count; i++)
            {
                String nickname = t.getListPlayers().ElementAt(i).Nickname;
                String sqlParticipation = "INSERT INTO participations" +
                                            "VALUES(" + idT + ", " + GetPlayerID(nickname) + ");";
                command = new SqlCommand(sqlParticipation, c);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserisce i dati iniziali del turno
        /// </summary>
        /// <param name="t"> istanza di torneo </param>
        /// <param name="rn"> numero del turno (roundNumber) </param>
        /// <returns></returns>
        public bool InsertTurnStart(Tournament t, int rn)
        {
            bool success = true;
            SqlCommand command;
            int idTable;
            int idT = GetTournamentID(t.Name);
            int players_n = t.CurrentTurn.TablePlayersNumber;
            String sqlInsertTable = "";
            String sqlInsertComposition = "";
            myConnection.Open();
            try
            {
                for (int i = 0; i < t.CurrentTurn.getListTables().Count; i++)
                {
                    sqlInsertTable = "INSERT INTO tables(id_tournament, players_n, turn, progressive_n)" +
                                            "VALUES(" + idT + ", " +
                                                       players_n + ", " +
                                                       rn + ", " +
                                                       t.CurrentTurn.getListTables().ElementAt(i).TableNumber + ");";
                    idTable = GetTableID(rn);
                    for (int k = 0; k < t.CurrentTurn.getListTables().ElementAt(i).getPlayers().Count; k++)
                    {
                        String playerNickname = t.CurrentTurn.getListTables().ElementAt(i).getPlayers().ElementAt(k).Nickname;
                        sqlInsertComposition = "INSERT INTO compositions(id_table, id_player)" +
                                                    "VALUES(" + idTable +
                                                            "'" + playerNickname + ");";
                    }
                    command = new SqlCommand(sqlInsertTable, myConnection);
                    command.ExecuteNonQuery();
                    command = new SqlCommand(sqlInsertComposition, myConnection);
                    command.ExecuteNonQuery();
                }
            } catch(Exception e)
            {
                success = false;
            }
            myConnection.Close();

            return success;
        }
    }
}
