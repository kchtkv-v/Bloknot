using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bloknot2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        Bloknot bloknot;

        public MainWindow()
        {
            InitializeComponent();
            bloknot = new Bloknot(richTextBox);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            bloknot.Create();
            this.Title = bloknot.NameFile;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            bloknot.Modified = true;
        }

        private void newWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
           newWindow.Show();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;
            }
            if (bloknot.Open() == false) return;
            this.Title = bloknot.NameFile;
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            bloknot.ASaveBloknot();
            this.Title = bloknot.NameFile;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;
            }
            else this.Close();
        }

        private void StatusBar_Click(object sender, RoutedEventArgs e)
        {
            if (statusBar.Visibility == Visibility.Visible)
            {
                statusBar.Visibility = Visibility.Collapsed;
                richTextBox.Margin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                statusBar.Visibility = Visibility.Visible;
                richTextBox.Margin = new Thickness(0, 0, 0, 22);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (bloknot.NameFile == "")
            {
                bloknot.ASaveBloknot();
                this.Title = bloknot.NameFile;
            }
            else
            {
                bloknot.Save();
            }
            this.Title = bloknot.NameFile;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Undo();
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Cut();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Paste();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.Selection.Text = "";
        }
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            richTextBox.SelectAll();
        }
        private void ChangeLines_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task(richTextBox);
            task.Show();
        }
        DispatcherTimer timer;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.IsEnabled = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            time.Text = d.ToString("HH:mm:ss");
            data.Text = d.ToString("dd:MM:yyyy");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (bloknot.Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (bloknot.ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;

            }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №17\nВыполнила студентка группы ИСП-31 Кочеткова В\nВвод и редактирование текста.\n2.Сохранение текста.Название сохраненного текста должно выводиться в строке заголовке.При повторном сохранении, имя файла не запрашивать.\n3.Сохранение с новым именем.\n4.Загрузка текста из файла.\n5.Завершение работы с программой.При завершении если текст не сохранен, запроситьсохранение.\n6.Создание нового документа.\n7.Справка о программе.\n8.Реализовать действия с буфером обмена.\n9.Реализовать отмену последнего действия.\n10.Реализовать контекстное меню для действий с буфером обмена.\n11.Очистить текст. Переставить указанные строки в тексте.", "Справка", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
