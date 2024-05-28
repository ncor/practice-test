using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PracticeTestApp
{
    public partial class MainWindow : Window
    {
        // Список вопросов
        private List<Question> questions;
        // Индекс текущего вопроса
        private int currentQuestionIndex = 0;
        // Переменная для хранения счета
        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            // Загрузка вопросов при запуске приложения
            LoadQuestions();
            // Отображение первого вопроса
            DisplayQuestion();
        }

        // Метод для загрузки вопросов
        private void LoadQuestions()
        {
            // Создание списка вопросов
            questions = new List<Question>
            {
                // Инициализация каждого вопроса
                new Question
                {
                    Title = "Прочитайте текст. Вставьте пропущенные буквы. Укажите все цифры, на месте которых пишется буква Е",
                    Detail = "В течени..(1) многих веков, тысяч..(2)летий меняется форма и высота земной поверхност.(3) Там, где раньше шумело море, впоследстви.(4) образовалась суша. Сильно разрушаются горные породы, состоящи.(5) из нескольких частей. Эти части расш..(6))ряются и сж.(7))маются, поэтому между ними образуются трещ.(8) ны. В них попадает вода. При замерзании она увелич..(9)вается в объёме и разрывает самые твёрдые камни.",
                    Options = new List<Option>
                    {
                        new Option { Text = "125", IsCorrect = true },
                        new Option { Text = "123", IsCorrect = false },
                        new Option { Text = "56", IsCorrect = false }
                    }
                },
                new Question
                {
                    Title = "Раскройте скобки и запишите имя собственное «Жюль Верн» в соответствующей форме, соблюдая нормы современного русского литературного языка",
                    Detail = "В сюжетах романов (Жюль Верн) рассказывается о приключениях героев.",
                    Options = new List<Option>
                    {
                        new Option { Text = "Жюля Верна", IsCorrect = true },
                        new Option { Text = "Жюль Верн", IsCorrect = false },
                        new Option { Text = "Жюль Верне", IsCorrect = false }
                    }
                },
                new Question
                {
                    Title = "Замените словосочетание «кружевной шарф», построенное на основе согласования, синонимичным словосочетанием со связью управление",
                    Detail = "Напишите получившееся словосочетание.",
                    Options = new List<Option>
                    {
                        new Option { Text = "шарф из кружева", IsCorrect = true },
                        new Option { Text = "кружево из шарфа", IsCorrect = false },
                        new Option { Text = "шарф из кружево", IsCorrect = false }
                    }
                },
                new Question
                {
                    Title = "Расставьте знаки препинания. Укажите цифры, на месте которых должны стоять запятые",
                    Detail = "Заядлые путешественники (1) ищущие (2) что посмотреть в России самого необычного и даже мистического (3) непременно должны спуститься в Кунгурскую пещеру. Сегодня это место (4) самое известное уральское чудо.",
                    Options = new List<Option>
                    {
                        new Option { Text = "123", IsCorrect = true },
                        new Option { Text = "234", IsCorrect = false },
                        new Option { Text = "124", IsCorrect = false }
                    }
                },
                new Question
                {
                    Title = "Укажите варианты ответов, в которых средством выразительности речи является фразеологизм",
                    Detail = "Запишите номера ответов.\n1)По поведению росомахи служители зоопарка с первого взгляда поняли, что у неё, наверно, скоро должны родиться детёныши и она ищет место для логова.\n2)Особенно волновалась росомаха, когда они подходили к соседней клетке, в которой сидели два злющих волка.\n5)Она изо всех сил кидалась то на одного, то на другого волка, увёртывалась от их укусов, бросалась опять, не давая им подойти к детям.",
                    Options = new List<Option>
                    {
                        new Option { Text = "15", IsCorrect = true },
                        new Option { Text = "12", IsCorrect = false },
                        new Option { Text = "35", IsCorrect = false }
                    }
                }
            };
        }

        // Метод для отображения текущего вопроса
        private void DisplayQuestion()
        {
            // Если индекс текущего вопроса превышает количество вопросов,
            // значит, тест завершен, и нужно показать результат
            if (currentQuestionIndex >= questions.Count)
            {
                DisplayResult();
                return;
            }

            // Получение текущего вопроса из списка
            Question currentQuestion = questions[currentQuestionIndex];
            // Отображение заголовка текущего вопроса
            QuestionTitleTextBlock.Text = currentQuestion.Title;
            // Отображение подробного условия текущего вопроса
            QuestionDetailTextBlock.Text = currentQuestion.Detail;
            // Очистка предыдущих вариантов ответов
            OptionsStackPanel.Children.Clear();

            // Добавление радиокнопок для каждого варианта ответа
            foreach (var option in currentQuestion.Options)
            {
                RadioButton rb = new RadioButton
                {
                    Content = option.Text,
                    Tag = option
                };
                OptionsStackPanel.Children.Add(rb);
            }
        }

        // Обработчик события нажатия кнопки "Следующий вопрос"
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение текущего вопроса
            Question currentQuestion = questions[currentQuestionIndex];
            // Поиск выбранного варианта ответа
            var selectedOption = OptionsStackPanel.Children.OfType<RadioButton>()
                                    .FirstOrDefault(rb => rb.IsChecked == true);

            // Если выбран верный вариант ответа, увеличить счет
            if (selectedOption != null && ((Option)selectedOption.Tag).IsCorrect)
            {
                score++;
            }

            // Переход к следующему вопросу
            currentQuestionIndex++;
            // Отображение следующего вопроса
            DisplayQuestion();
        }

        // Метод для отображения результатов тестирования
        private void DisplayResult()
        {
            // Очистка панели основного стека
            MainStackPanel.Children.Clear();
            // Создание текстового блока для отображения результатов
            TextBlock resultTextBlock = new TextBlock
            {
                // Форматирование строки с результатами
                Text = $"Ваш результат: {score} из {questions.Count}",
                FontSize = 24,
                FontWeight = FontWeights.SemiBold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 30, 0, 0)
            };
            MainStackPanel.Children.Add(resultTextBlock);
            // Добавления изображения
            Image resultImage = new Image
            {
                Source = new BitmapImage(new Uri("https://sun9-20.userapi.com/impg/4oAZ1rUSjuJw1Cxpw01p5bYwGBclYa7BdCPUzQ/vZTpVC1cyxU.jpg?size=450x442&quality=95&sign=3839d7533f822cddc3477fc05f175323&type=album")),
                Width = 300,
                Height = 300,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };
            MainStackPanel.Children.Add(resultImage);
        }
    }

    // Сущность вопроса
    public class Question
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public List<Option> Options { get; set; }
    }
    
    // Сущность варианта ответа
    public class Option
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
