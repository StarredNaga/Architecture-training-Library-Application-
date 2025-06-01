using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Entitites;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MyWpfApp;

public partial class MainWindow : Window
{
    private readonly Library _library;

    private readonly ObservableCollection<BookDto?> _books = [];
    private BookDto? _selectedBook;

    public MainWindow()
    {
        InitializeComponent();

        var configurations = new Configurations.Configurations();

        var serviceProvider = configurations.UseDataBase();

        _library = new Library(serviceProvider.GetRequiredService<IBookService>());
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadBooks();
    }

    private async void LoadBooks()
    {
        _books.Clear();
        foreach (var book in await _library.GetBooks(null))
        {
            _books.Add(book);
        }
        BooksListView.ItemsSource = _books;
    }

    private void ShowAddBookView(object sender, RoutedEventArgs e)
    {
        BooksListPanel.Visibility = Visibility.Collapsed;
        AddBookPanel.Visibility = Visibility.Visible;
        EditBookPanel.Visibility = Visibility.Collapsed;

        // Очистка полей
        TxtTitle.Text = "";
        TxtImageUrl.Text = "";
        TxtAuthor.Text = "";
        TxtDescription.Text = "";
    }

    private void ShowBooksListView(object sender, RoutedEventArgs e)
    {
        BooksListPanel.Visibility = Visibility.Visible;
        AddBookPanel.Visibility = Visibility.Collapsed;
        EditBookPanel.Visibility = Visibility.Collapsed;
    }

    private void ShowEditBookView(object sender, RoutedEventArgs e)
    {
        if (_selectedBook == null)
            return;

        BooksListPanel.Visibility = Visibility.Collapsed;
        AddBookPanel.Visibility = Visibility.Collapsed;
        EditBookPanel.Visibility = Visibility.Visible;

        // Заполнение полей данными выбранной книги
        TxtEditTitle.Text = _selectedBook.Title;
        TxtEditImageUrl.Text = _selectedBook.ImageUrl;
        TxtEditAuthor.Text = _selectedBook.Author;
        TxtEditDescription.Text = _selectedBook.Description;
    }

    private void CancelOperation(object sender, RoutedEventArgs e)
    {
        ShowBooksListView(sender, e);
    }

    private async void SaveBook(object sender, RoutedEventArgs e)
    {
        var newBook = new BookDto
        {
            Title = TxtTitle.Text,
            ImageUrl = TxtImageUrl.Text,
            Author = TxtAuthor.Text,
            Description = TxtDescription.Text,
        };

        var createdBook = await _library.CreateBook(newBook);

        _books.Add(createdBook);

        ShowBooksListView(sender, e);
    }

    private async void UpdateBook(object sender, RoutedEventArgs e)
    {
        if (_selectedBook == null)
            return;

        var updatedBook = new BookDto
        {
            Title = TxtEditTitle.Text,
            ImageUrl = TxtEditImageUrl.Text,
            Author = TxtEditAuthor.Text,
            Description = TxtEditDescription.Text,
        };

        try
        {
            var result = await _library.UpdateBook(_selectedBook, updatedBook);

            // Обновляем книгу в коллекции
            var index = _books.IndexOf(_selectedBook);

            if (index >= 0)
            {
                _books[index] = result;
            }

            ShowBooksListView(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Ошибка обновления: {ex.Message}",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }

    private async void DeleteSelectedBook(object sender, RoutedEventArgs e)
    {
        try
        {
            await _library.DeleteBook(_selectedBook);

            _books.Remove(_selectedBook);

            _selectedBook = null;

            UpdateButtonStates();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Ошибка удаления: {ex.Message}",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }

    private void BooksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selectedBook = BooksListView.SelectedItem as BookDto;
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        BtnEditBook.IsEnabled = _selectedBook != null;
        BtnDeleteBook.IsEnabled = _selectedBook != null;
    }
}
