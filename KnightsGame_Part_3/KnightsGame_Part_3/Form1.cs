using System;                           // Verwendung des Systems
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;                   //  alle blass hinterlegten using-Anweisungen wurden zwar auch automatisch erzeugt, sind jedoch unnötig und deswegen auch deaktiviert
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;             // Verwendung von grafischen Benutzeroberflächen (GUIs) und Formularen sowie den dazugehörigen Komponenten (Werkzeugen)

namespace KnightsGame_Part_3            // Projektbezeichnung
{
    public partial class fm_KnightsGame_3 : Form    // Formular zur grafischen Benutzerschnittstelle (GUI - graphical user interface) KnightsGame als öffentliche Teil-/Unterklasse
    {
        public fm_KnightsGame_3()                   // öffentliche Klasse (public) KnightsGame_3 (Ritterspiel_3)
        {
            InitializeComponent();                  // Komponenten der Klasse werden initialisiert
        }


    // Funktion zum Erzeugen bzw. Laden des Formulars zur grafischen Benutzerschnittstelle (GUI - graphical user interface)
        private void fm_KnightsGame_3_Load(object sender, EventArgs e)
        {
            tB_diceNumber.ReadOnly = true;      // Eingabefeld (Textbox) für die Würfelzahl enthält nur Leserechte (Es darf nichts eingetragen werden.)
            tB_knightNumber.ReadOnly = true;    // Eingabefeld (Textbox) für die Ritternummer enthält nur Leserechte (Es darf nichts eingetragen werden.)
         // Als Erster ist immer Ritter 1 dran
            bt_knight1.Enabled = true;          // Schaltfläche (Button) für Ritter 1 ist aktiviert.
            bt_knight2.Enabled = false;         // Schaltfläche (Button) für Ritter 2 ist deaktiviert.
            bt_knight3.Enabled = false;         // Schaltfläche (Button) für Ritter 3 ist deaktiviert.
            bt_knight4.Enabled = false;         // Schaltfläche (Button) für Ritter 4 ist deaktiviert.
            bt_knight5.Enabled = false;         // Schaltfläche (Button) für Ritter 5 ist deaktiviert. 
        }


     // Festlegen der Energiepunkte für jeden der 5 Ritter als globale Variablen, da sonst die darauffolgenden Button-OnClick-Events(Ereignisse) nur einmal
     // ausgeführt werden und eine Schleife alle Zwischenschritte automatisch durchgeht, sodass am Ende nur das Ergebnis des letztmöglichen Schrittes angezeigt wird  
     // Jeder Ritter hat anfangs 100 Energiepunkte.
        int energyPoints_Knight_1 = 100;
        int energyPoints_Knight_2 = 100;
        int energyPoints_Knight_3 = 100;
        int energyPoints_Knight_4 = 100;
        int energyPoints_Knight_5 = 100;

        
     // Funktion während eines Button-OnClick-Events beim Klick auf den Button bt_knight1 -> Ritter 1 ist mit Würfeln dran
        private void bt_knight1_Click(object sender, EventArgs e)
        {
            Random random = new Random();               // Erzeugen eines Zufallsgenerators als Objekt

            int diceNumber = random.Next(1, 7);         // Würfelzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 6 generiert, Endwert 7, weil bei Endwert 6 nur 1-5
            tB_diceNumber.Text = diceNumber.ToString(); // Würfelzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String (Zeichenkette) umgewandelt ausgegeben 
            bt_knight1.Enabled = false;                 // bt_knight1 wird deaktiviert
            
            int knightNumber = random.Next(1, 6);       // Ritterzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 5 generiert, Endwert 6, weil bei Endwert 5 nur 1-4
            tB_knightNumber.Text = knightNumber.ToString(); // Ritterzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String umgewandelt ausgegeben

            switch(knightNumber)
            {
                case 1:
                    
                 // Wenn Ritter 1 als Einziger noch im Spiel ist und alle anderen Ritter tot sind, da sie alle 0 Energiepunkte haben
                    if (int.Parse(bt_knight1.Text) > 0 &&
                        int.Parse(bt_knight2.Text) == 0 &&
                        int.Parse(bt_knight3.Text) == 0 &&
                        int.Parse(bt_knight4.Text) == 0 &&
                        int.Parse(bt_knight5.Text) == 0 )
                    {
                        MessageBox.Show("Knight 1 has won.");  // Hinweisfenster angezeigt: Ritter 1 hat gewonnen, da alle anderen Ritter tot sind
                        MessageBox.Show("Game over!");         // weiteres Hinweisfenster (MessageBox): Spiel ist damit zu Ende -> Schließen eines Hinweisfensters mit Klick auf OK
                        bt_knight1.Enabled = true;             // Gewinner-Button bt_knight1 wird aktiviert, jedoch werden nach dem Spielende bei jedem Klick immer nur
                                                               // die beiden Hinweisfenster zum Sieg von Ritter 1 bzw. Ende des Spiels angezeigt
                    }

                    else
                    { // Falls Ritter 1 zufällig ausgewählt wird, erscheint ein Hinweisfenster (MessageBox), welches ausgibt, dass Ritter 1 gerade dran war
                         MessageBox.Show("Knight 1 was already on turn!");
                         bt_knight1.Enabled = true; // bt_knight1 wird aktiviert
                    }
                    break; // Bedingung für case 1 wird beendet

                case 2:
                    energyPoints_Knight_2 -= diceNumber;        // Ritter 2 wird die vom Ritter 1 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_2 > 0)              // wenn Ritter 2 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight2.Enabled = true;                          // bt_knight2 wird aktiviert
                        bt_knight2.Text = energyPoints_Knight_2.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 2 wird auf bt_knight2 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 2 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight2.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight2 den Wert 0 als String aus.
                        bt_knight2.Enabled = false; // bt_knight2 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight3.Enabled = true;
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                    }
                break; // Bedingung für case 2 wird beendet


                case 3:
                    energyPoints_Knight_3 -= diceNumber;        // Ritter 3 wird die vom Ritter 1 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_3 > 0)              // wenn Ritter 3 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight3.Enabled = true;                          // bt_knight3 wird aktiviert
                        bt_knight3.Text = energyPoints_Knight_3.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 3 wird auf bt_knight3 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 2 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight3.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight2 den Wert 0 als String aus.
                        bt_knight3.Enabled = false; // bt_knight2 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                break; // Bedingung für case 3 wird beendet


                case 4:
                    energyPoints_Knight_4 -= diceNumber;        // Ritter 4 wird die vom Ritter 1 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_4 > 0)              // wenn Ritter 4 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight4.Enabled = true;                          // bt_knight4 wird aktiviert
                        bt_knight4.Text = energyPoints_Knight_4.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 4 wird auf bt_knight4 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 4 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight4.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight4 den Wert 0 als String aus.
                        bt_knight4.Enabled = false; // bt_knight4 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                        bt_knight3.Enabled = true;
                    }
               break; // Bedingung für case 4 wird beendet


               case 5:
                    energyPoints_Knight_5 -= diceNumber;        // Ritter 5 wird die vom Ritter 1 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_5 > 0)  // wenn Ritter 5 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight5.Enabled = true;                          // bt_knight5 wird aktiviert
                        bt_knight5.Text = energyPoints_Knight_5.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 5 wird auf bt_knight5 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 5 keine Energiepunkte mehr hat und somit tot ist)                                            
                    {
                        bt_knight5.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight5 den Wert 0 als String aus.
                        bt_knight5.Enabled = false; // bt_knight5 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight4.Enabled = true;
                    }
               break; // Bedingung für case 5 wird beendet

               default: break;
              // sonst wird die switch-case-Bedingung beendet (In diesem Fall findet bei default nichts statt, da bei jedem Buttonklick eine Ritterzahl ausgegeben wird.)
            } 
        }

        private void bt_knight2_Click(object sender, EventArgs e)
        {
            Random random = new Random();               // Erzeugen eines Zufallsgenerators als Objekt

            int diceNumber = random.Next(1, 7);         // Würfelzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 6 generiert, Endwert 7, weil bei Endwert 6 nur 1-5
            tB_diceNumber.Text = diceNumber.ToString(); // Würfelzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String (Zeichenkette) umgewandelt ausgegeben 
            bt_knight2.Enabled = false;                 // bt_knight2 wird deaktiviert

            int knightNumber = random.Next(1, 6);       // Ritterzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 5 generiert, Endwert 6, weil bei Endwert 5 nur 1-4
            tB_knightNumber.Text = knightNumber.ToString(); // Ritterzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String umgewandelt ausgegeben

            switch (knightNumber)
            {
                case 2:

                    // Wenn Ritter 2 als Einziger noch im Spiel ist und alle anderen Ritter tot sind, da sie alle 0 Energiepunkte haben
                    if (int.Parse(bt_knight2.Text) > 0 &&
                        int.Parse(bt_knight3.Text) == 0 &&
                        int.Parse(bt_knight4.Text) == 0 &&
                        int.Parse(bt_knight5.Text) == 0 &&
                        int.Parse(bt_knight1.Text) == 0)
                    {
                        MessageBox.Show("Knight 2 has won.");  // Hinweisfenster angezeigt: Ritter 2 hat gewonnen, da alle anderen Ritter tot sind
                        MessageBox.Show("Game over!");         // weiteres Hinweisfenster (MessageBox): Spiel ist damit zu Ende -> Schließen eines Hinweisfensters mit Klick auf OK
                        bt_knight2.Enabled = true;             // Gewinner-Button bt_knight2 wird aktiviert, jedoch werden nach dem Spielende bei jedem Klick immer nur
                                                               // die beiden Hinweisfenster zum Sieg von Ritter 2 bzw. Ende des Spiels angezeigt
                    }

                    else
                    { // Falls Ritter 2 zufällig ausgewählt wird, erscheint ein Hinweisfenster (MessageBox), welches ausgibt, dass Ritter 2 gerade dran war
                         MessageBox.Show("Knight 2 was already on turn!");
                        bt_knight2.Enabled = true; // bt_knight2 wird aktiviert
                    }
                    break; // Bedingung für case 2 wird beendet

                case 1:
                    energyPoints_Knight_1 -= diceNumber;        // Ritter 1 wird die vom Ritter 2 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_1 > 0)              // wenn Ritter 1 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight1.Enabled = true;                          // bt_knight1 wird aktiviert
                        bt_knight1.Text = energyPoints_Knight_1.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 1 wird auf bt_knight1 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 1 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight1.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight1 den Wert 0 als String aus.
                        bt_knight1.Enabled = false; // bt_knight1 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight2.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                    }
                    break; // Bedingung für case 1 wird beendet


                case 3:
                    energyPoints_Knight_3 -= diceNumber;        // Ritter 3 wird die vom Ritter 2 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_3 > 0)              // wenn Ritter 3 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight3.Enabled = true;                          // bt_knight3 wird aktiviert
                        bt_knight3.Text = energyPoints_Knight_3.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 3 wird auf bt_knight3 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 2 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight3.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight2 den Wert 0 als String aus.
                        bt_knight3.Enabled = false; // bt_knight2 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 3 wird beendet


                case 4:
                    energyPoints_Knight_4 -= diceNumber;        // Ritter 4 wird die vom Ritter 2 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_4 > 0)              // wenn Ritter 4 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight4.Enabled = true;                          // bt_knight4 wird aktiviert
                        bt_knight4.Text = energyPoints_Knight_4.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 4 wird auf bt_knight4 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 4 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight4.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight4 den Wert 0 als String aus.
                        bt_knight4.Enabled = false; // bt_knight4 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight3.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 4 wird beendet


                case 5:
                    energyPoints_Knight_5 -= diceNumber;       // Ritter 5 wird die vom Ritter 2 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_5 > 0)             // wenn Ritter 5 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight5.Enabled = true;                          // bt_knight5 wird aktiviert
                        bt_knight5.Text = energyPoints_Knight_5.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 5 wird auf bt_knight5 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 5 keine Energiepunkte mehr hat und somit tot ist)                                            
                    {
                        bt_knight5.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight5 den Wert 0 als String aus.
                        bt_knight5.Enabled = false; // bt_knight5 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 5 wird beendet

                default: break;
                    // sonst wird die switch-case-Bedingung beendet (In diesem Fall findet bei default nichts statt, da bei jedem Buttonklick eine Ritterzahl ausgegeben wird.)
            }
        }

        private void bt_knight3_Click(object sender, EventArgs e)
        {
            Random random = new Random();               // Erzeugen eines Zufallsgenerators als Objekt

            int diceNumber = random.Next(1, 7);         // Würfelzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 6 generiert, Endwert 7, weil bei Endwert 6 nur 1-5
            tB_diceNumber.Text = diceNumber.ToString(); // Würfelzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String (Zeichenkette) umgewandelt ausgegeben 
            bt_knight3.Enabled = false;                 // bt_knight3 wird deaktiviert

            int knightNumber = random.Next(1, 6);       // Ritterzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 5 generiert, Endwert 6, weil bei Endwert 5 nur 1-4
            tB_knightNumber.Text = knightNumber.ToString(); // Ritterzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String umgewandelt ausgegeben

            switch (knightNumber)
            {
                case 3:

                    // Wenn Ritter 3 als Einziger noch im Spiel ist und alle anderen Ritter tot sind, da sie alle 0 Energiepunkte haben
                    if (int.Parse(bt_knight3.Text) > 0 &&
                        int.Parse(bt_knight4.Text) == 0 &&
                        int.Parse(bt_knight5.Text) == 0 &&
                        int.Parse(bt_knight1.Text) == 0 &&
                        int.Parse(bt_knight2.Text) == 0)
                    {
                        MessageBox.Show("Knight 3 has won.");  // Hinweisfenster angezeigt: Ritter 3 hat gewonnen, da alle anderen Ritter tot sind
                        MessageBox.Show("Game over!");         // weiteres Hinweisfenster (MessageBox): Spiel ist damit zu Ende -> Schließen eines Hinweisfensters mit Klick auf OK
                        bt_knight3.Enabled = true;             // Gewinner-Button bt_knight3 wird aktiviert, jedoch werden nach dem Spielende bei jedem Klick immer nur
                                                               // die beiden Hinweisfenster zum Sieg von Ritter 3 bzw. Ende des Spiels angezeigt
                    }

                    else
                    { // Falls Ritter 3 zufällig ausgewählt wird, erscheint ein Hinweisfenster (MessageBox), welches ausgibt, dass Ritter 3 gerade dran war
                         MessageBox.Show("Knight 3 was already on turn!");
                         bt_knight3.Enabled = true; // bt_knight3 wird aktiviert
                    }
                    break; // Bedingung für case 3 wird beendet

                case 2:
                    energyPoints_Knight_2 -= diceNumber;        // Ritter 2 wird die vom Ritter 3 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_2 > 0)              // wenn Ritter 2 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight2.Enabled = true;                          // bt_knight2 wird aktiviert
                        bt_knight2.Text = energyPoints_Knight_2.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 2 wird auf bt_knight2 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 2 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight2.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight2 den Wert 0 als String aus.
                        bt_knight2.Enabled = false; // bt_knight2 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight3.Enabled = true;
                    }
                    break; // Bedingung für case 2 wird beendet


                case 1:
                    energyPoints_Knight_1 -= diceNumber;        // Ritter 1 wird die vom Ritter 3 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_1 > 0)              // wenn Ritter 1 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight1.Enabled = true;                          // bt_knight1 wird aktiviert
                        bt_knight1.Text = energyPoints_Knight_1.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 1 wird auf bt_knight1 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 1 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight1.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight1 den Wert 0 als String aus.
                        bt_knight1.Enabled = false; // bt_knight1 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 1 wird beendet


                case 4:
                    energyPoints_Knight_4 -= diceNumber;        // Ritter 4 wird die vom Ritter 3 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_4 > 0)              // wenn Ritter 4 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight4.Enabled = true;                          // bt_knight4 wird aktiviert
                        bt_knight4.Text = energyPoints_Knight_4.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 4 wird auf bt_knight4 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 4 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight4.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight4 den Wert 0 als String aus.
                        bt_knight4.Enabled = false; // bt_knight4 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight3.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 4 wird beendet


                case 5:
                    energyPoints_Knight_5 -= diceNumber;        // Ritter 5 wird die vom Ritter 3 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_5 > 0)              // wenn Ritter 5 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight5.Enabled = true;                          // bt_knight5 wird aktiviert
                        bt_knight5.Text = energyPoints_Knight_5.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 5 wird auf bt_knight5 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 5 keine Energiepunkte mehr hat und somit tot ist)                                            
                    {
                        bt_knight5.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight5 den Wert 0 als String aus.
                        bt_knight5.Enabled = false; // bt_knight5 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 5 wird beendet

                default: break;
            // sonst wird die switch-case-Bedingung beendet (In diesem Fall findet bei default nichts statt, da bei jedem Buttonklick eine Ritterzahl ausgegeben wird.)
            }
        }

        private void bt_knight4_Click(object sender, EventArgs e)
        {
            Random random = new Random();               // Erzeugen eines Zufallsgenerators als Objekt

            int diceNumber = random.Next(1, 7);         // Würfelzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 6 generiert, Endwert 7, weil bei Endwert 6 nur 1-5
            tB_diceNumber.Text = diceNumber.ToString(); // Würfelzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String (Zeichenkette) umgewandelt ausgegeben 
            bt_knight4.Enabled = false;                 // bt_knight4 wird deaktiviert

            int knightNumber = random.Next(1, 6);       // Ritterzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 5 generiert, Endwert 6, weil bei Endwert 5 nur 1-4
            tB_knightNumber.Text = knightNumber.ToString(); // Ritterzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String umgewandelt ausgegeben

            switch (knightNumber)
            {
                case 4:

                    // Wenn Ritter 4 als Einziger noch im Spiel ist und alle anderen Ritter tot sind, da sie alle 0 Energiepunkte haben
                    if (int.Parse(bt_knight4.Text) > 0 &&
                        int.Parse(bt_knight5.Text) == 0 &&
                        int.Parse(bt_knight1.Text) == 0 &&
                        int.Parse(bt_knight2.Text) == 0 &&
                        int.Parse(bt_knight3.Text) == 0)
                    {
                        MessageBox.Show("Knight 4 has won.");  // Hinweisfenster angezeigt: Ritter 4 hat gewonnen, da alle anderen Ritter tot sind
                        MessageBox.Show("Game over!");         // weiteres Hinweisfenster (MessageBox): Spiel ist damit zu Ende -> Schließen eines Hinweisfensters mit Klick auf OK
                        bt_knight4.Enabled = true;             // Gewinner-Button bt_knight4 wird aktiviert, jedoch werden nach dem Spielende bei jedem Klick immer nur
                                                               // die beiden Hinweisfenster zum Sieg von Ritter 4 bzw. Ende des Spiels angezeigt
                    }

                    else
                    { // Falls Ritter 4 zufällig ausgewählt wird, erscheint ein Hinweisfenster (MessageBox), welches ausgibt, dass Ritter 4 gerade dran war
                         MessageBox.Show("Knight 4 was already on turn!");
                         bt_knight4.Enabled = true; // bt_knight4 wird aktiviert
                    }
                    break; // Bedingung für case 4 wird beendet

                case 2:
                    energyPoints_Knight_2 -= diceNumber;        // Ritter 2 wird die vom Ritter 4 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_2 > 0)              // wenn Ritter 2 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight2.Enabled = true;                          // bt_knight2 wird aktiviert
                        bt_knight2.Text = energyPoints_Knight_2.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 2 wird auf bt_knight2 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 2 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight2.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight2 den Wert 0 als String aus.
                        bt_knight2.Enabled = false; // bt_knight2 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight3.Enabled = true;
                    }
                    break; // Bedingung für case 2 wird beendet


                case 1:
                    energyPoints_Knight_1 -= diceNumber;        // Ritter 1 wird die vom Ritter 4 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_1 > 0)              // wenn Ritter 1 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight1.Enabled = true;                          // bt_knight1 wird aktiviert
                        bt_knight1.Text = energyPoints_Knight_1.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 1 wird auf bt_knight1 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 1 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight1.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight1 den Wert 0 als String aus.
                        bt_knight1.Enabled = false; // bt_knight1 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 1 wird beendet


                case 3:
                    energyPoints_Knight_3 -= diceNumber;        // Ritter 3 wird die vom Ritter 4 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_3 > 0)              // wenn Ritter 3 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight3.Enabled = true;                          // bt_knight3 wird aktiviert
                        bt_knight3.Text = energyPoints_Knight_3.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 3 wird auf bt_knight3 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 3 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight3.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight3 den Wert 0 als String aus.
                        bt_knight3.Enabled = false; // bt_knight3 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 3 wird beendet


                case 5:
                    energyPoints_Knight_5 -= diceNumber;        // Ritter 5 wird die vom Ritter 4 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_5 > 0)              // wenn Ritter 5 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight5.Enabled = true;                          // bt_knight5 wird aktiviert
                        bt_knight5.Text = energyPoints_Knight_5.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 5 wird auf bt_knight5 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 5 keine Energiepunkte mehr hat und somit tot ist)                                            
                    {
                        bt_knight5.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight5 den Wert 0 als String aus.
                        bt_knight5.Enabled = false; // bt_knight5 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight3.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 5 wird beendet

                default: break;
               // sonst wird die switch-case-Bedingung beendet (In diesem Fall findet bei default nichts statt, da bei jedem Buttonklick eine Ritterzahl ausgegeben wird.)
            }
        }

        private void bt_knight5_Click(object sender, EventArgs e)
        {
            Random random = new Random();               // Erzeugen eines Zufallsgenerators als Objekt

            int diceNumber = random.Next(1, 7);         // Würfelzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 6 generiert, Endwert 7, weil bei Endwert 6 nur 1-5
            tB_diceNumber.Text = diceNumber.ToString(); // Würfelzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String (Zeichenkette) umgewandelt ausgegeben 
            bt_knight5.Enabled = false;                 // bt_knight5 wird deaktiviert

            int knightNumber = random.Next(1, 6);       // Ritterzahl wird bei jedem Buttonklick als Zufallszahl zwischen 1 und 5 generiert, Endwert 6, weil bei Endwert 5 nur 1-4
            tB_knightNumber.Text = knightNumber.ToString(); // Ritterzahl wird als ganzzahliger int-Wert in Textbox übetragen und dort als String umgewandelt ausgegeben

            switch (knightNumber)
            {
                case 5:

                 // Wenn Ritter 5 als Einziger noch im Spiel ist und alle anderen Ritter tot sind, da sie alle 0 Energiepunkte haben
                    if (int.Parse(bt_knight5.Text) > 0 &&
                        int.Parse(bt_knight1.Text) == 0 &&
                        int.Parse(bt_knight2.Text) == 0 &&
                        int.Parse(bt_knight3.Text) == 0 &&
                        int.Parse(bt_knight4.Text) == 0)
                    {
                        MessageBox.Show("Knight 5 has won.");  // Hinweisfenster angezeigt: Ritter 5 hat gewonnen, da alle anderen Ritter tot sind
                        MessageBox.Show("Game over!");         // weiteres Hinweisfenster (MessageBox): Spiel ist damit zu Ende -> Schließen eines Hinweisfensters mit Klick auf OK
                        bt_knight5.Enabled = true;             // Gewinner-Button bt_knight5 wird aktiviert, jedoch werden nach dem Spielende bei jedem Klick immer nur
                                                               // die beiden Hinweisfenster zum Sieg von Ritter 5 bzw. Ende des Spiels angezeigt
                    }

                    else
                    { // Falls Ritter 5 zufällig ausgewählt wird, erscheint ein Hinweisfenster (MessageBox), welches ausgibt, dass Ritter 5 gerade dran war
                         MessageBox.Show("Knight 5 was already on turn!");
                         bt_knight5.Enabled = true; // bt_knight5 wird aktiviert
                    }
                    break; // Bedingung für case 5 wird beendet

                case 2:
                    energyPoints_Knight_2 -= diceNumber;        // Ritter 2 wird die vom Ritter 5 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_2 > 0)              // wenn Ritter 2 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight2.Enabled = true;                          // bt_knight2 wird aktiviert
                        bt_knight2.Text = energyPoints_Knight_2.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 2 wird auf bt_knight2 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 2 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight2.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight2 den Wert 0 als String aus.
                        bt_knight2.Enabled = false; // bt_knight2 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight3.Enabled = true;
                    }
                    break; // Bedingung für case 2 wird beendet


                case 1:
                    energyPoints_Knight_1 -= diceNumber;        // Ritter 1 wird die vom Ritter 5 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_1 > 0)              // wenn Ritter 1 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight1.Enabled = true;                          // bt_knight1 wird aktiviert
                        bt_knight1.Text = energyPoints_Knight_1.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 1 wird auf bt_knight1 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 1 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight1.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight1 den Wert 0 als String aus.
                        bt_knight1.Enabled = false; // bt_knight1 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight2.Enabled = true;
                        bt_knight3.Enabled = true;
                    }
                    break; // Bedingung für case 1 wird beendet

                case 3:
                    energyPoints_Knight_3 -= diceNumber;        // Ritter 3 wird die vom Ritter 5 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_3 > 0)              // wenn Ritter 3 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight3.Enabled = true;                          // bt_knight3 wird aktiviert
                        bt_knight3.Text = energyPoints_Knight_3.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 3 wird auf bt_knight3 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 3 keine Energiepunkte mehr hat und somit tot ist)                                            
                    {
                        bt_knight3.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight3 den Wert 0 als String aus.
                        bt_knight3.Enabled = false; // bt_knight3 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet
                        bt_knight4.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight2.Enabled = true;
                    }
                    break; // Bedingung für case 3 wird beendet

                case 4:
                    energyPoints_Knight_4 -= diceNumber;        // Ritter 4 wird die vom Ritter 5 gewürfelte Menge der Energiepunkte entnommen

                    if (energyPoints_Knight_4 > 0)              // wenn Ritter 4 mehr als 0 Energiepunkte hat und damit noch im Spiel ist
                    {
                        bt_knight4.Enabled = true;                          // bt_knight4 wird aktiviert
                        bt_knight4.Text = energyPoints_Knight_4.ToString(); // Anzahl der verbliebenen Energiepunkte von Ritter 4 wird auf bt_knight4 als String umgewandelt ausgegeben.
                    }

                    else                            // sonst (wenn Ritter 4 keine Energiepunkte mehr hat und somit tot ist) 
                    {
                        bt_knight4.Text = "0";      // Selbst, wenn es durch die Würfelzahl zu einem negativen Ergebnis gekommen wäre, gibt bt_knight4 den Wert 0 als String aus.
                        bt_knight4.Enabled = false; // bt_knight4 wird deaktiviert

                     // Alle anderen Buttons werden hingegen eingeblendet, solange die dort gekennzeichneten Ritter noch Energiepunkte haben
                        bt_knight2.Enabled = true;
                        bt_knight5.Enabled = true;
                        bt_knight1.Enabled = true;
                        bt_knight3.Enabled = true;
                    }
                    break; // Bedingung für case 4 wird beendet


                default: break;
                    // sonst wird die switch-case-Bedingung beendet (In diesem Fall findet bei default nichts statt, da bei jedem Buttonklick eine Ritterzahl ausgegeben wird.)
            }
        }
    }
}