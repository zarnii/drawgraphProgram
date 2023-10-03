using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    /// <summary>
    /// Вершина.
    /// </summary>
    class Vertex
    {
        /// <summary>
        /// Координаты.
        /// </summary>
        public int x, y;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">X координата вершины.</param>
        /// <param name="y">Y координата вершины.</param>
        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// Связь вершин.
    /// </summary>
    class Edge
    {
        /// <summary>
        /// Связываемые вершины.
        /// </summary>
        public int v1, v2;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    /// <summary>
    /// Функционал отрисовка графа.
    /// </summary>
    class DrawGraph
    {
        /// <summary>
        /// Поле для рисования.
        /// </summary>
        Bitmap bitmap;

        /// <summary>
        /// Черная ручка.
        /// </summary>
        Pen blackPen;

        /// <summary>
        /// Красная ручка.
        /// </summary>
        Pen redPen;

        /// <summary>
        /// Холодно-черная ручка.
        /// </summary>
        Pen darkGoldPen;

        /// <summary>
        /// 
        /// </summary>
        Graphics gr;

        /// <summary>
        /// Шрифт.
        /// </summary>
        Font fo;

        /// <summary>
        /// Заливка объектов.
        /// </summary>
        Brush br;

        /// <summary>
        /// Точка.
        /// </summary>
        PointF point;

        public int R = 20; //радиус окружности вершины

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="width">Ширина поля.</param>
        /// <param name="height">Высота поля.</param>
        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 2;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        /// <summary>
        /// Получения рисунка на bitmap.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap()
        {
            return bitmap;
        }


        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        /// <summary>
        /// Рисование вершины.
        /// </summary>
        /// <param name="x">X координата.</param>
        /// <param name="y">Y координата.</param>
        /// <param name="number">Номер вершины.</param>
        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }

		/// <summary>
		/// Обводка выделеной вершины.
		/// </summary>
		/// <param name="x">X координата.</param>
		/// <param name="y">Y координата.</param>
		public void drawSelectedVertex(int x, int y)
        {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        /// <summary>
        /// Соединение вершин.
        /// </summary>
        /// <param name="V1">Первая вершина.</param>
        /// <param name="V2">Вторая вершина.</param>
        /// <param name="E">Связь.</param>
        /// <param name="numberE">Номер связи.</param>
        public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                gr.DrawString(((char)('a' + numberE)).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }

        /// <summary>
        /// Отрисовка всего графа.
        /// </summary>
        /// <param name="V">Лист веришин.</param>
        /// <param name="E">Лист связей.</param>
        public void drawALLGraph(List<Vertex> V, List<Edge> E)
        {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
            }
            //рисуем вершины
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }

        //заполняет матрицу смежности
        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }

        //заполняет матрицу инцидентности
        public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < E.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, i] = 1;
                matrix[E[i].v2, i] = 1;
            }
        }

        
    }
}