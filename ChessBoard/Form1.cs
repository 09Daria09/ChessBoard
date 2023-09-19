using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessBoard
{
    public partial class Form1 : Form
    {
        int boardSize = 8;
        int cellSize = 100; 
        double pieceScaleFactor = 0.75;
        double majorPieceScaleFactor = 1;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.Size = new Size(cellSize * boardSize + 16, cellSize * boardSize + 38);
            pictureBox1.Dock = DockStyle.Fill;
            this.Text = "(-Chess-)";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Color cellColor = (i + j) % 2 == 0 ? Color.White : Color.Black;
                    using (SolidBrush brush = new SolidBrush(cellColor))
                    {
                        e.Graphics.FillRectangle(brush, i * cellSize, j * cellSize, cellSize, cellSize);
                    }
                }
            }

            Image PiecesImageWhite = Properties.Resources.White_Chess;
            Image PiecesImageBlack = Properties.Resources.Black_Chess;

            Rectangle sourceWhitePawnRect = new Rectangle(2775, 2354, 738, 1214);
            Rectangle sourceBlackPawnRect = new Rectangle(2775, 2450, 738, 1216);

            for (int i = 0; i < boardSize; i++)
            {
                DrawPiece(e.Graphics, PiecesImageWhite, sourceWhitePawnRect, i, 6, cellSize);
                DrawPiece(e.Graphics, PiecesImageBlack, sourceBlackPawnRect, i, 1, cellSize);
            }

            Rectangle sourceBlackKing = new Rectangle(2711, 200, 867, 1945);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackKing, 4, 0, cellSize);
            Rectangle sourceWhiteKing = new Rectangle(2711, 200, 867, 1945); 
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhiteKing, 4, 7, cellSize);

            Rectangle sourceWhiteQueen = new Rectangle(3578, 8, 874, 2139);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhiteQueen, 3, 7, cellSize);
            Rectangle sourceBlackQueen = new Rectangle(3578, 8, 874, 2139);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackQueen, 3, 0, cellSize);

            Rectangle sourceWhiteRook1 = new Rectangle(63, 696, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhiteRook1, 0, 7, cellSize);
            Rectangle sourceBlackRook1 = new Rectangle(63, 696, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackRook1, 0, 0, cellSize);
            Rectangle sourceWhiteRook2 = new Rectangle(63, 696, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhiteRook2, 7, 7, cellSize);
            Rectangle sourceBlackRook2 = new Rectangle(63, 696, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackRook2, 7, 0, cellSize);

            Rectangle sourceWhiteKnight1 = new Rectangle(948, 683, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhiteKnight1, 6, 7, cellSize);
            Rectangle sourceBlackKnight1 = new Rectangle(948, 683, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackKnight1, 6, 0, cellSize);
            Rectangle sourceWhiteKnight2 = new Rectangle(948, 683, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhiteKnight2, 1, 7, cellSize);
            Rectangle sourceBlackKnight2 = new Rectangle(948, 683, 823, 1498);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackKnight2, 1, 0, cellSize);

            Rectangle sourceWhitePrimaryQueen1 = new Rectangle(1774, 289, 924, 1850);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhitePrimaryQueen1, 5, 7, cellSize);
            Rectangle sourceBlackPrimaryQueen1 = new Rectangle(1774, 289, 924, 1850);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackPrimaryQueen1, 5, 0, cellSize);
            Rectangle sourceWhitePrimaryQueen2 = new Rectangle(1774, 289, 924, 1850);
            DrawMajorPiece(e.Graphics, PiecesImageWhite, sourceWhitePrimaryQueen2, 2, 7, cellSize);
            Rectangle sourceBlackPrimaryQueen2 = new Rectangle(1774, 289, 924, 1850);
            DrawMajorPiece(e.Graphics, PiecesImageBlack, sourceBlackPrimaryQueen2, 2, 0, cellSize);
        }

        private void DrawPiece(Graphics g, Image image, Rectangle sourceRect, int x, int y, int cellSize)
        {
            double sourceAspectRatio = sourceRect.Width / (double)sourceRect.Height;
            double destHeight = cellSize * pieceScaleFactor;
            double destWidth = destHeight * sourceAspectRatio;

            if (destWidth > cellSize)
            {
                destWidth = cellSize;
                destHeight = destWidth / sourceAspectRatio;
            }

            double destX = x * cellSize + (cellSize - destWidth) / 2;
            double destY = y * cellSize + (cellSize - destHeight) / 2;

            Rectangle destinationRect = new Rectangle((int)destX, (int)destY, (int)destWidth, (int)destHeight);
            g.DrawImage(image, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }
        private void DrawMajorPiece(Graphics g, Image image, Rectangle sourceRect, int x, int y, int cellSize)
        {
            double sourceAspectRatio = sourceRect.Width / (double)sourceRect.Height;
            double destHeight = cellSize * majorPieceScaleFactor;
            double destWidth = destHeight * sourceAspectRatio;

            if (destWidth > cellSize)
            {
                destWidth = cellSize;
                destHeight = destWidth / sourceAspectRatio;
            }

            double destX = x * cellSize + (cellSize - destWidth) / 2;
            double destY = y * cellSize + (cellSize - destHeight) / 2;

            Rectangle destinationRect = new Rectangle((int)destX, (int)destY, (int)destWidth, (int)destHeight);
            g.DrawImage(image, destinationRect, sourceRect, GraphicsUnit.Pixel);
        }


    }
}
