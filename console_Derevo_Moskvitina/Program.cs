using System;
using System.Diagnostics;
using System.Drawing;


namespace console_Derevo_Moskvitino
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 1920; // ширина изображения
            int height = 1080; // высота изображения

            //работа пользователя с деревом
            Console.Write("Введите наклон дерева, учитывйте, наклон измеряется в градусах (желательно указать значение 2, чтобы дерево было ровным): ");
            double v_angle = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите угол наклона ПЕРВОЙ ветви (стандарт - 4): ");
            double v_ang1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите угол наклона ВТОРОЙ ветви (стандарт - 6): ");
            double v_ang2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите координату дерева, где оно будет находится по горизонтали (стандарт - 2): ");
            double v_x = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите координату дерева, где оно будет находится по вертикали (стандарт - 100): ");
            double v_y = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите длину дерева (стандарт - 3, лучше от 3 до 6): ");
            double v_a = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите цвет из предложенных (Red, Blue, Green, White, Yellow, Pink, Purple, Orange, Gold): ");
            string color = Console.ReadLine();
            Color userColor = Color.FromName(color);

            Bitmap bitmap = new Bitmap(width, height); // создаем Bitmap окно 

            using (Graphics g = Graphics.FromImage(bitmap)) // создаем объект Graphics чтобы можно было рисовать
            {
                // очищаем изображение, и ставим белый цвет

                Pen pen = new Pen(userColor); // создаем Pen

                // параметры дерева
                double angle = Math.PI / v_angle; //поворот дерева
                double ang1 = Math.PI / v_ang1; //угол наклона ветвей (первой)
                double ang2 = Math.PI / v_ang2; //угол наклона ветвей (второй)
                double x = width / v_x; //координата X точки, по горизонтали
                double y = height - v_y; //координата Y точки, по вертикали
                double a = height / v_a; //размер дерева ЛУЧШЕ БЛЯТЬ 3-10

                // отрисовываем дерево
                DrawTree(g, pen, x, y, a, angle, ang1, ang2);
            }

            // сохраняем изображение в файл
            bitmap.Save("derevo_Moskvitina.png", System.Drawing.Imaging.ImageFormat.Png);

            //обработчик загрузки фото
            try
            {
                Console.WriteLine("\nФрактал 'Дерево Москвитина' был создан и сохранент в файл pythagoras_tree.png");
                Console.WriteLine("\nПодождите загружаю файл...\nПодождите загружаю файл...\nПодождите загружаю файл...");
                Process.Start("derevo_Moskvitina.png");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Произошла не предвиденная ситуация");
                Console.ReadLine();
            }
            
        }

        static void DrawTree(Graphics g, Pen p, double x, double y, double a, double angle, double ang1, double ang2)
        {
            if (a > 2)
            {
                a *= 0.7; //уменьшаем длину ветвей, чтоб компьюетер справился с загрузкой
                /*уменьшает длину ветвей на 30% на каждой итерации рекурсии. Это необходимо для того, 
                 * чтобы избежать переполнения стека вызовов при отрисовке больших и сложных деревьев.
                 * Уменьшение длины ветвей позволяет уменьшить количество рекурсивных вызовов 
                 * функции и глубину стека вызовов, что уменьшает */


                double xnew = x + a * Math.Cos(angle); // Вычисляем новую координату X
                double ynew = y - a * Math.Sin(angle); // Вычисляем новую координату Y

                g.DrawLine(p, (float)x, (float)y, (float)xnew, (float)ynew);

                DrawTree(g, p, xnew, ynew, a, angle + ang1, ang1, ang2);
                DrawTree(g, p, xnew, ynew, a, angle - ang2, ang1, ang2);

                

            }
        }
    }
}
