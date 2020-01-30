using bilbiotecaSoWindows.Common;
using bilbiotecaSoWindows.DataAccess;
using bilbiotecaSoWindows.Model;
using bilbiotecaSoWindows.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace bilbiotecaSoWindows.ViewModel
{
    public class LibrarianViewModel : BaseViewModel
    {
        #region constructor
        // receber a Window aberta no construtor para o caso de instruções já ter a window correta em vez de ter de criar uma instrução para encontrar a Window ativa
        public LibrarianViewModel(Window LibrarianWindow)
        {
            _LibrarianWindow = LibrarianWindow;
            // carregar as reservas para a Grid por defeito, todos os que a data de entrega ou de reserva esperada é superior à data atual
            loadReservations();
            // comandos para abrir Window indicada
            registerPickUp = new Command(pickUpWindow);
            deliveryBook = new Command(deliveryWindow);
            createNewUser = new Command(newUserWindow);
            createNewBook = new Command(newBookWindow);
            createReservation = new Command(newReservationWindow);
            // comandos para executar consultas à base de dados
            allBooksCommand = new Command(allBooksList);
            allUsersCommand = new Command(allUsersList);
            reservationsCommand = new Command(loadReservations);
            searchUserCommand = new Command(searchUserList);
            searchBookCommand = new Command(searchBookList);
            allIrregularCommand = new Command(allIrregularList);
            // comando para fechar a janela 
            logOutCommand = new Command(doLogOut);
        }
        #endregion

        #region variables

        #region private

        #region ObservableCollections
        private ObservableCollection<Book> _allBooks { get; set; }
        private ObservableCollection<User> _allUsers { get; set; }
        private ObservableCollection<Reservation> _reservations { get; set; }
        #endregion

        #region booleans
        private bool _selectedAllBooks { get; set; }
        private bool _selectedAllUsers { get; set; }
        private bool _selectedAllIrregulars { get; set; }
        private bool _reservationsLoad { get; set; }
        #endregion

        #region primitives
        private string _searchUser { get; set; }
        private string _searchBook { get; set; }
        private string _dataActual;
        #endregion

        private DataAccessQuery query = new DataAccessQuery();
        private DataSet resultadoQuery = new DataSet();
        private Window _LibrarianWindow;

        #endregion

        #region public    
        #region primitives      
        public string searchUser
        {
            get { return _searchUser; }
            set
            {
                if (_searchUser != value)
                {
                    _searchUser = value;
                    NotifyOnPropertyChanged("searchUser");
                }
            }
        }
        public string searchBook
        {
            get { return _searchBook; }
            set
            {
                if (_searchBook != value)
                {
                    _searchBook = value;
                    NotifyOnPropertyChanged("searchBook");
                }
            }
        }
        #endregion

        #region booleans
        public bool selectedAllBooks
        {
            get { return _selectedAllBooks; }
            set
            {
                if (_selectedAllBooks != value)
                {
                    _selectedAllBooks = value;
                    NotifyOnPropertyChanged("selectedAllBooks");
                }
            }
        }
        public bool selectedAllIrregulars
        {
            get { return _selectedAllIrregulars; }
            set
            {
                if (_selectedAllIrregulars != value)
                {
                    _selectedAllIrregulars = value;
                    NotifyOnPropertyChanged("selectedAllIrregulars");
                }
            }
        }
        public bool selectedAllUsers
        {
            get { return _selectedAllUsers; }
            set
            {
                if (_selectedAllUsers != value)
                {
                    _selectedAllUsers = value;
                    NotifyOnPropertyChanged("selectedAllUsers");
                }
            }
        }
        public bool reservationsLoad
        {
            get { return _reservationsLoad; }
            set
            {
                if (_reservationsLoad != value)
                {
                    _reservationsLoad = value;
                    NotifyOnPropertyChanged("reservationsLoad");
                }
            }
        }
        #endregion

        #region ObservableCollections
        public ObservableCollection<Book> allBooks
        {
            get { return _allBooks; }
            set
            {
                if (_allBooks != value)
                {
                    _allBooks = value;
                    NotifyOnPropertyChanged("allBooks");
                }

            }
        }
        public ObservableCollection<User> allUsers
        {
            get { return _allUsers; }
            set
            {
                if (_allUsers != value)
                {
                    _allUsers = value;
                    NotifyOnPropertyChanged("allUsers");
                }

            }
        }
        public ObservableCollection<Reservation> reservations
        {
            get { return _reservations; }
            set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                    NotifyOnPropertyChanged("reservations");
                }
            }
        }       
        #endregion

        #region command
        public Command createNewUser { get; set; }
        public Command createNewBook { get; set; }
        public Command createReservation { get; set; }
        public Command allBooksCommand { get; set; }
        public Command allUsersCommand { get; set; }
        public Command logOutCommand { get; set; }
        public Command searchUserCommand { get; set; }
        public Command searchBookCommand { get; set; }
        public Command allIrregularCommand { get; set; }
        public Command reservationsCommand { get; set; }
        public Command registerPickUp { get; set; }
        public Command deliveryBook { get; set; }
        #endregion
        #endregion

        #endregion

        #region methods
        #region newWindow
        private void deliveryWindow()
        {
            // A lista é passada como argumento para que o novo objeto adicionado seja adicionado à lista existente e seja refletido 
            // na tabela correspondente caso ela esteja visivel. Vai ser reutilizado o boleano porque esta lista é a predefinida 
            // que vai ser atualizada sem a atualização que passou a devolvida;
            _reservationsLoad = true;
            selectedListFill("reservationsForDelivery", "");

            DeliveryView openNewReservation = new DeliveryView(reservations);
            openNewReservation.Show();
            
        }

        private void pickUpWindow()
        {

        }

        private void newUserWindow()
        {
            // A lista é passada como argumento para que o novo objeto adicionado seja adicionado à lista existente e seja refletido 
            // na tabela correspondente caso ela esteja visivel. Para evitar exceção de Null, caso esta esteja vazia é preenchida e para evitar
            // alterações no UI não é levantada nenhuma notificação, como por exemplo o check nos submenus estão com binding ao boleano presente no else
            if (allUsers != null)
            {
                NewUserView openNewUser = new NewUserView(allUsers);
                openNewUser.Show();
            }
            else
            {
                _selectedAllUsers = true;
                selectedListFill("users", "");

                NewUserView openNewUser = new NewUserView(allUsers);
                openNewUser.Show();
            }
        }
        private void newReservationWindow()
        {
            // A lista é passada como argumento para que o novo objeto adicionado seja adicionado à lista existente e seja refletido 
            // na tabela correspondente caso ela esteja visivel. Para evitar exceção de Null, caso esta esteja vazia é preenchida e para evitar
            // alterações no UI não é levantada nenhuma notificação, como por exemplo o check nos submenus estão com binding ao boleano presente no else
            if (reservations != null)
            {
                reservationView openNewReservation = new reservationView(reservations);
                openNewReservation.Show();
            }
            else
            {
                _dataActual = DateTime.Now.ToString("s");

                _reservationsLoad = true;
                selectedListFill("reservations", _dataActual);

                reservationView openNewReservation = new reservationView(reservations);
                openNewReservation.Show();
            }
        }
        private void newBookWindow()
        {
            // A lista é passada como argumento para que o novo objeto adicionado seja adicionado à lista existente e seja refletido 
            // na tabela correspondente caso ela esteja visivel. Para evitar exceção de Null, caso esta esteja vazia é preenchida e para evitar
            // alterações no UI não é levantada nenhuma notificação, como por exemplo o check nos submenus estão com binding ao boleano presente no else
            if (allBooks != null)
            {
                NewBookView openNewBook = new NewBookView(allBooks);
                openNewBook.Show();
            }
            else
            {
                _selectedAllBooks = true;
                selectedListFill("books", "");

                NewBookView openNewBook = new NewBookView(allBooks);
                openNewBook.Show();
            }
        }
        #endregion


        private void allBooksList()
        {
            // boleanos para indicar no método selectedListFill qual a collection a preencher apesar de no selectedListFill após ser validado
            // estado este é alterado para o estado false manteve-se aqui como reforço
            _reservationsLoad = false;
            _selectedAllBooks = true;
            _selectedAllUsers = false;
            _selectedAllIrregulars = false;

            // avisos usados para atribuir o comportamento de mutualmente exclusivo à propriedade IsCheckable do Menu Item
            NotifyOnPropertyChanged("selectedAllBooks");
            NotifyOnPropertyChanged("selectedAllUsers");
            NotifyOnPropertyChanged("selectedAllIrregulars");
            NotifyOnPropertyChanged("reservationsLoad");

            if (_allBooks == null)
            {
                selectedListFill("books", "");
            }
            
            // Notificar que ObservableCollection está preenchida 
            NotifyOnPropertyChanged("allBooks");
        }
        private void allUsersList()
        {
            // boleanos para indicar no método selectedListFill qual a collection a preencher apesar de no selectedListFill após ser validado
            // estado este é alterado para o estado false manteve-se aqui como reforço
            _reservationsLoad = false;
            _selectedAllUsers = true;
            _selectedAllBooks = false;
            _selectedAllIrregulars = false;

            // avisos usados para atribuir o comportamento de mutualmente exclusivo à propriedade IsCheckable do Menu Item
            NotifyOnPropertyChanged("selectedAllBooks");
            NotifyOnPropertyChanged("selectedAllUsers");
            NotifyOnPropertyChanged("selectedAllIrregulars");
            NotifyOnPropertyChanged("reservationsLoad");

            if (_allUsers == null)
            {
                selectedListFill("users", "");
            }
            // Notificar que ObservableCollection está preenchida 
            NotifyOnPropertyChanged("allUsers");
        }
        private void allIrregularList()
        {
            // s de standard dateTime format formato internacional para que o formato local possa ser compreendido pelo SQL
            _dataActual = DateTime.Now.ToString("s");

            // boleanos para indicar no método selectedListFill qual a collection a preencher apesar de no selectedListFill após ser validado
            // estado este é alterado para o estado false manteve-se aqui como reforço
            _reservationsLoad = false;
            _selectedAllUsers = false;
            _selectedAllBooks = false;
            _selectedAllIrregulars = true;


            // avisos usados para atribuir o comportamento de mutualmente exclusivo à propriedade IsCheckable do Menu Item
            NotifyOnPropertyChanged("selectedAllBooks");
            NotifyOnPropertyChanged("selectedAllUsers");
            NotifyOnPropertyChanged("selectedAllIrregulars");
            NotifyOnPropertyChanged("reservationsLoad");

            if (_reservations == null)
            {
                selectedListFill("irregulars", _dataActual);
            }            
            // Notificar que ObservableCollection está preenchida, vai ser reutilizada a reservatins porque os campos a preencher são os mesmos 
            // e porque as duas tabelas nunca são visiveis ao mesmo tempo
            NotifyOnPropertyChanged("reservations");

        }
        private void loadReservations()
        {   // s de standard dateTime format formato internacional para que o formato local possa ser compreendido pelo SQL
            _dataActual = DateTime.Now.ToString("s");

            // boleanos para indicar no método selectedListFill qual a collection a preencher apesar de no selectedListFill após ser validado
            // estado este é alterado para o estado false manteve-se aqui como reforço
            _reservationsLoad = true;
            _selectedAllUsers = false;
            _selectedAllBooks = false;
            _selectedAllIrregulars = false;

            // avisos usados para atribuir o comportamento de mutualmente exclusivo à propriedade IsCheckable do Menu Item
            NotifyOnPropertyChanged("selectedAllBooks");
            NotifyOnPropertyChanged("selectedAllUsers");
            NotifyOnPropertyChanged("selectedAllIrregulars");
            NotifyOnPropertyChanged("reservationsLoad");

            if (_reservations == null)
            {
                selectedListFill("reservations", _dataActual);
            }
            // Notificar que ObservableCollection está preenchida.
            NotifyOnPropertyChanged("reservations");
        }
        private void searchUserList()
        {
            // boleanos para indicar no método selectedListFill qual a collection a preencher apesar de no selectedListFill após ser validado
            // estado este é alterado para o estado false manteve-se aqui como reforço
            _reservationsLoad = false;
            _selectedAllUsers = true;
            _selectedAllBooks = false;
            _selectedAllIrregulars = false;

            // avisos usados para atribuir o comportamento de mutualmente exclusivo à propriedade IsCheckable do Menu Item
            NotifyOnPropertyChanged("selectedAllBooks");
            NotifyOnPropertyChanged("selectedAllUsers");
            NotifyOnPropertyChanged("selectedAllIrregulars");
            NotifyOnPropertyChanged("reservationsLoad");

            if (_allUsers == null)
            {
                selectedListFill("searchUsers", _searchUser);
            }

            // Notificar que ObservableCollection está preenchida.

            NotifyOnPropertyChanged("allUsers");
        }
        private void searchBookList()
        {
            // boleanos para indicar no método selectedListFill qual a collection a preencher
            _reservationsLoad = false;
            _selectedAllUsers = false;
            _selectedAllBooks = true;
            _selectedAllIrregulars = false;

            NotifyOnPropertyChanged("selectedAllBooks");
            NotifyOnPropertyChanged("selectedAllUsers");
            NotifyOnPropertyChanged("selectedAllIrregulars");
            NotifyOnPropertyChanged("reservationsLoad");

            selectedListFill("searchBooks", _searchBook);
            NotifyOnPropertyChanged("allBooks");
        }

        // Preencher Collections
        public void selectedListFill(string choice, string search)
        {
            if (!String.IsNullOrEmpty(choice))
            {
                resultadoQuery = query.consultDataBase(choice, search);

                if (_selectedAllBooks == true)
                {
                    _selectedAllBooks = false;
                    _allBooks = new ObservableCollection<Book>();

                    foreach (DataRow row in resultadoQuery.Tables[0].Rows)
                    {
                        _allBooks.Add(new Book()
                        {
                            isbn = row[0].ToString(),
                            title = row[1].ToString(),
                            year = Convert.ToInt32(row[2].ToString()),
                            publisher = row[3].ToString(),
                            isAvailable = Convert.ToBoolean(row[4].ToString()),
                            author = new Author()
                            {
                                firstNameAuthor = row[5].ToString(),
                                lastNameAuthor = row[6].ToString(),
                            }
                        }
                                        );
                    }
                }
                else if (_selectedAllUsers == true)
                {
                    _selectedAllUsers = false;
                    _allUsers = new ObservableCollection<User>();

                    foreach (DataRow row in resultadoQuery.Tables[0].Rows)
                    {
                        _allUsers.Add(new User()
                        {
                            userGuid = row[0].ToString(),
                            firstName = row[1].ToString(),
                            lastName = row[2].ToString(),
                            phoneNumber = row[3].ToString(),
                            email = row[4].ToString(),
                            userLogin = new UserLogin()
                            {
                                username = row[5].ToString()
                            }
                        });
                    }
                }
                // este if é para preencher duas listas de reservas diferentes porque as duas não são visualizadas ao mesmo tempo e são 
                // visiveis no mesmo local
                else if (_reservationsLoad == true || _selectedAllIrregulars == true)
                {
                    _reservationsLoad = false;
                    _selectedAllIrregulars = false;
                    _reservations = new ObservableCollection<Reservation>();
       
                    foreach (DataRow row in resultadoQuery.Tables[0].Rows)
                    {
                        _reservations.Add(new Reservation()
                        {
                            reservationId = row[0].ToString(),
                            reservationUser = row[1].ToString(),
                            usernameNomeProprio = row[2].ToString(),
                            usernameApelido = row[3].ToString(),
                            reservationBook = row[4].ToString(),
                            bookTitle = row[5].ToString(),
                            reservationDateTime = (DateTime)row[6],
                            expectedDeliveryDateTime = (DateTime)row[7],
                            observations = row[8].ToString(),
                        });
                    }
                }
                else if (_selectedAllIrregulars == true)
                {
                    _selectedAllIrregulars = false;
                }
            }
        }

        #region logout
        public void doLogOut()
        {
            if ((Application.Current.Windows.OfType<Window>().Count(x => x.IsActive)) == 1)
            {
                if (MessageBox.Show("Sair?", "Texto", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
                {
                    _LibrarianWindow.Close();
                }
            }
        }
        #endregion

        #endregion
    }
}
