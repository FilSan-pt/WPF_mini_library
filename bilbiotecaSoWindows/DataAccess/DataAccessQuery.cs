using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using bilbiotecaSoWindows.Model;
using System.Windows.Documents;
using System.Windows;

namespace bilbiotecaSoWindows.DataAccess
{
    public class DataAccessQuery
    {
        #region properties 
        #region private
        private DataSet _queryResult = new DataSet();
        private SqlConnection _sqlConnection = new SqlConnection();
        #endregion
        #endregion

        #region constructor
        public DataAccessQuery() { }
        #endregion

        #region ConnectionToDb Methods

        // Devolve o DataSet de resultado da query
        private DataSet sqlConnectionBase(string sqlQuery, string sqlTable)
        {
            try
            {
                _queryResult = new DataSet();
                _sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["connectionDB"].ToString();
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlQuery;

                cmd.Connection = _sqlConnection;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(_queryResult, sqlTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return _queryResult;
        }

        // executar comandos de inserção, update, delete
        private void sqlConnectionExecute(string sqlQuery)
        {

            try
            {
                _queryResult = new DataSet();
                _sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["connectionDB"].ToString();
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = _sqlConnection.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        #endregion

        #region others
        public int authenticateUser(string usernameIn, string passwordIn)
        {

            string query = "SELECT* FROM userLogin WHERE userLogin.userName = '" + usernameIn + "' AND userLogin.password = '" + passwordIn + "' AND userLogin.isActive = 1;";
            _queryResult = sqlConnectionBase(query, "userLogin");

            if (_queryResult.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            // devolve a chave estrangeiro do papel exemplo 3 equivale ao role bibliotecario
            return Convert.ToInt32(_queryResult.Tables[0].Rows[0][2].ToString());
        }

        public bool bookAvailabitily(string isbnIn) 
        {

            string query = "SELECT * FROM books WHERE books.isbn = '" + isbnIn + "' AND books.isAvailable = 1;";
            _queryResult = sqlConnectionBase(query, "books");

            if (_queryResult.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else if (_queryResult.Tables[0].Rows[0][4].ToString().Contains("0"))
                {
                    return false;
                }

            return true;
        }

        public void bookDelivery(Reservation reservation, string currentDate)
        {
            string query = "UPDATE reservations SET dateTimeDelivery = '" + currentDate + "' WHERE reservations.bookReservation = '" + reservation.reservationBook + "'; ";
            sqlConnectionExecute(query);

            query = "UPDATE books SET isAvailable = 1 WHERE books.isbn = '" + reservation.reservationBook + "';";
            sqlConnectionExecute(query);
        }
        #endregion

        #region Create
        public bool CreateUserDB(User userIn)
        {
            try
            {
                string query = "INSERT INTO userLogin VALUES ('" + userIn.userLogin.username + "','" + userIn.userLogin.password + "','1','1');";
                sqlConnectionExecute(query);

                //===========================================

                Guid novoGuidUser = Guid.NewGuid();

                query = "INSERT INTO users VALUES ('" + novoGuidUser + "', '" + userIn.firstName + "', '" + userIn.lastName + "', '" + userIn.phoneNumber + "', '" + userIn.email + "', '" + userIn.userLogin.username + "');";
                sqlConnectionExecute(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateBookDB(Book bookIn)
        {
            try
            {
                Guid novoGuidAuthor = Guid.NewGuid();

                string query = "INSERT INTO authors VALUES ('" + novoGuidAuthor + "','" + bookIn.author.firstNameAuthor + "','" + bookIn.author.lastNameAuthor + "');";
                sqlConnectionExecute(query);
                //===========================================

                query = "INSERT INTO books VALUES ('" + bookIn.isbn + "', '" + bookIn.title + "', '" + bookIn.publisher + "', '" + bookIn.year + "', '1');";
                sqlConnectionExecute(query);
                //===========================================

                query = "INSERT INTO authorBook VALUES ('" + novoGuidAuthor + "', '" + bookIn.isbn + "');";
                sqlConnectionExecute(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CreateReservationDB(Reservation reservationIN)
        {
            if(!bookAvailabitily(reservationIN.reservationBook)) { return 1; }
            // Corrigir limit minímo de admissibilidade
            if (reservationIN.reservationDateTime.Year < 1900)
            {
                reservationIN.reservationDateTime = DateTime.Parse("1/1/1901 12:00:00 AM");
            }

            if (reservationIN.pickUpDateTime.Year < 1900)
            {
                reservationIN.pickUpDateTime = DateTime.Parse("1/1/1901 12:00:00 AM");
            }

            if (reservationIN.expectedDeliveryDateTime.Year < 1900)
            {
                reservationIN.expectedDeliveryDateTime = DateTime.Parse("1/1/1901 12:00:00 AM");
            }

            if (reservationIN.deliveryDateTime.Year < 1900)
            {
                reservationIN.deliveryDateTime = DateTime.Parse("1/1/1901 12:00:00 AM");
            }

            // alterar datas para formato compatível com base daddos
            string _reservationDateTime = reservationIN.reservationDateTime.ToString("MM/dd/yyyy hh:mm:ss");
            string _pickUpDateTime = reservationIN.pickUpDateTime.ToString("MM/dd/yyyy hh:mm:ss");
            string _expectedDeliveryDateTime = reservationIN.expectedDeliveryDateTime.ToString("MM/dd/yyyy hh:mm:ss");
            string _deliveryDateTime = reservationIN.deliveryDateTime.ToString("MM/dd/yyyy hh:mm:ss"); 

            try
            {
                Guid pk = Guid.NewGuid();
                string query = "INSERT INTO reservations VALUES ('" + pk + "','"
                                + reservationIN.reservationUser + "','" + reservationIN.reservationBook + "','"
                                + _reservationDateTime + "','" + _pickUpDateTime + "','"
                                + _expectedDeliveryDateTime + "','" + _deliveryDateTime + "','"
                                + reservationIN.observations + "','1');";

                sqlConnectionExecute(query);

                query = "UPDATE books SET isAvailable = 0 WHERE books.isbn = '" + reservationIN.reservationBook + "';";
                sqlConnectionExecute(query);

                return 0;
            }
            catch
            {
                return 2;
            }
        }
        #endregion

        #region accessAll
        public DataSet consultDataBase(string choice, string search)
        {
            string query = null, table = null;

            switch (choice)
            {
                case "books":
                    query = "SELECT books.isbn,books.titulo, books.year, books.publisher, books.isAvailable, authors.firstName, authors.lastName "
                            + "FROM books INNER JOIN authorBook ON books.isbn = authorBook.isbn "
                            + "INNER JOIN authors ON authors.authorID = authorBook.authorID; ";
                    table = "book";
                    break;
                case "users":
                    query = "SELECT userID, firstName, lastName, phoneNumber, email, userName FROM users;";
                    table = "users";
                    break;
                case "searchBooks":
                    query = "SELECT books.isbn,books.titulo, books.year, books.publisher, books.isAvailable, authors.firstName, authors.lastName "
                            + "FROM books INNER JOIN authorBook ON books.isbn = authorBook.isbn "
                            + "INNER JOIN authors ON authors.authorID = authorBook.authorID "
                            + "WHERE books.isbn LIKE '%" + search + "%' OR books.titulo LIKE '%" + search + "%' OR books.publisher LIKE '%" + search + "%' "
                            + "OR authors.firstName LIKE '%" + search + "%' OR authors.lastName LIKE '%" + search + "%';";
                    table = "book";
                    break;
                case "searchUsers":
                    query = "SELECT users.userID, users.firstName, users.lastName, users.phoneNumber, users.email, users.userName "
                            + "FROM users WHERE users.firstName LIKE '%" + search + "%' OR users.lastName LIKE '%" + search + "%' OR "
                            + "users.phoneNumber LIKE '%" + search + "%' OR users.email LIKE '%" + search + "%' OR users.userName LIKE '%" + search + "%';";

                    table = "users";
                    break;
                case "reservations":
                    query = "SELECT reservations.reservationID, reservations.userReservation, users.firstName, users.lastName, "
                            + "reservations.bookReservation, books.titulo, reservations.dateTimereservation, reservations.dateTimeExpectedDelivery, reservations.observations "
                            + "FROM reservations "
                            + "INNER JOIN users ON users.userName = reservations.userReservation INNER JOIN books ON books.isbn = reservations.bookReservation "
                            + "WHERE reservations.dateTimereservation >= '" + search + "' OR reservations.dateTimeExpectedDelivery >= '" + search + "' AND books.isAvailable = 0;";

                    table = "reservations";
                    break;
                case "irregulars":
                    query = "SELECT reservations.reservationID, reservations.userReservation, users.firstName, users.lastName, reservations.bookReservation, books.titulo, "
                            + "reservations.dateTimereservation, reservations.dateTimeExpectedDelivery, reservations.observations "
                            + "FROM reservations "
                            + "INNER JOIN users ON users.userName = reservations.userReservation INNER JOIN books ON books.isbn = reservations.bookReservation "
                            + "WHERE reservations.dateTimeExpectedDelivery < '" + search + "' AND books.isAvailable = 0;";

                    table = "reservations";
                    break;
                case "reservationsForDelivery":
                    query = "SELECT reservations.reservationID, reservations.userReservation, users.firstName, users.lastName, reservations.bookReservation, "
                            + "books.titulo, reservations.dateTimereservation, reservations.dateTimeExpectedDelivery, reservations.observations " 
                            + "FROM reservations "
                            + "INNER JOIN users ON users.userName = reservations.userReservation INNER JOIN books ON books.isbn = reservations.bookReservation "
                            + "WHERE books.isAvailable = 0;";

                    table = "reservations";
                    break;
                default:
                    break;
            }

            return sqlConnectionBase(query, table);
        }
        #endregion

    }
}
