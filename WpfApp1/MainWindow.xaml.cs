using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                // Подобные инициализации для остальных вопросов
                // ...
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
