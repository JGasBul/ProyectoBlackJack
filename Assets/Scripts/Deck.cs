using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public Sprite[] faces;
    public GameObject dealer;
    public GameObject player;
    public Button hitButton;
    public Button stickButton;
    public Button playAgainButton;
    public Button x10Button;
    public Button x100Button;
    public Button x1000Button;
    public Text finalMessage;
    public Text probMessage;
    public Text pointsMessage;
    public Text dealerPointsMessage;
    public Text bank;
    public int bankValue;
    public int apuesta;
    public Text apuestaText;
    public int[] values = new int[52];
    int cardIndex = 0;

    private void Awake()
    {
        InitCardValues();
    }

    private void Start()
    {
        ShuffleCards();
        bank.text = "Tienes " + bankValue + " €";
        StartGame();
    }

    private void InitCardValues()
    {
        /*TODO:
         * Asignar un valor a cada una de las 52 cartas del atributo "values".
         * En principio, la posición de cada valor se deberá corresponder con la posición de faces. 
         * Por ejemplo, si en faces[1] hay un 2 de corazones, en values[1] debería haber un 2.
         */

        //Me hubiera gustado usar switch para un codigo mas limpio pero no me dejaba porque me decia que la caracteristica de 'patron racional' no estaba disponible en esta version.'

        for (int i = 0; i < values.Length; i++)
        {
            if (i == 0 || i == 13 || i == 26 || i == 39)
            {
                values[i] = 11;
            }
            else if (i < 10)
            {
                values[i] = i + 1;
            }
            else if (i < 13)
            {
                values[i] = 10;
            }
            else if (i < 23)
            {
                values[i] = i - 12;
            }
            else if (i < 26)
            {
                values[i] = 10;
            }
            else if (i < 36)
            {
                values[i] = i - 25;
            }
            else if (i < 39)
            {
                values[i] = 10;
            }
            else if (i < 48)
            {
                values[i] = i - 38;
            }
            else
            {
                values[i] = 10;
            }
        }
    }

    private void ShuffleCards()
    {
        /*TODO:
         * Barajar las cartas aleatoriamente.
         * El método Random.Range(0,n), devuelve un valor entre 0 y n-1
         * Si lo necesitas, puedes definir nuevos arrays.
         */
        for (int i = 0; i < values.Length; i++)
        {
            var j = Random.Range(0, 52);
            var temp_value = values[i];
            var temp_faces = faces[i];

            //Valor Random
            values[i] = values[j];
            values[j] = temp_value;

            //Face Random
            faces[i] = faces[j];
            faces[j] = temp_faces;

        }
    }

    void StartGame()
    {
        if (bankValue >= 10)
        {
            for (int i = 0; i < 2; i++)
            {
                PushPlayer();
                PushDealer();
                hitButton.interactable = false;
                stickButton.interactable = false;
                /*TODO:
                 * Si alguno de los dos obtiene Blackjack, termina el juego y mostramos mensaje
                 */

                pointsMessage.text = "Tienes " + player.GetComponent<CardHand>().points + " puntos";
                /*
                if (player.GetComponent<CardHand>().points == 21)
                {
                    finalMessage.text = "HAS GANADO";
                    hitButton.interactable = false;
                    stickButton.interactable = false;
                }

                else if (dealer.GetComponent<CardHand>().points == 21)
                {
                    finalMessage.text = "HAS PERDIDO";
                }
                */
            }
        }
        else
        {
            finalMessage.text = "Estas sin blanca amigo. GAME OVER. Pulsa nuevo juego para rellenar la banca";
            x10Button.interactable = false;
            x100Button.interactable = false;
            x1000Button.interactable = false;
            hitButton.interactable = false;
            stickButton.interactable = false;
            bankValue = 1000;
        }
    }

    private void CalculateProbabilities()
    {
        int playerPoints = player.GetComponent<CardHand>().points;
        if (playerPoints <= 11)
        {
            probMessage.text = "0%";
        }
        else if (playerPoints == 12)
        {
            probMessage.text = "31%";
        }
        else if (playerPoints == 13)
        {
            probMessage.text = "39%";
        } 
        else if (playerPoints == 14)
        {
            probMessage.text = "56%";
        }
        else if (playerPoints == 15)
        {
            probMessage.text = "58%";
        }
        else if (playerPoints == 16)
        {
            probMessage.text = "62%";
        }
        else if (playerPoints == 17)
        {
            probMessage.text = "69%";
        }
        else if (playerPoints == 18)
        {
            probMessage.text = "77%";
        }
        else if (playerPoints == 19)
        {
            probMessage.text = "85%";
        }
        else if (playerPoints == 20)
        {
            probMessage.text = "92%";
        }
        else
        {
            probMessage.text = "100%";
        }

        /*TODO:
         * Calcular las probabilidades de:
         * - Teniendo la carta oculta, probabilidad de que el dealer tenga más puntuación que el jugador
         * - Probabilidad de que el jugador obtenga entre un 17 y un 21 si pide una carta
         * - Probabilidad de que el jugador obtenga más de 21 si pide una carta          
         */
    }

    void PushDealer()
    {
        /*TODO:
         * Dependiendo de cómo se implemente ShuffleCards, es posible que haya que cambiar el índice.
         */
        dealer.GetComponent<CardHand>().Push(faces[cardIndex], values[cardIndex]);
        cardIndex++;
    }

    void PushPlayer()
    {
        /*TODO:
         * Dependiendo de cómo se implemente ShuffleCards, es posible que haya que cambiar el índice.
         */
        player.GetComponent<CardHand>().Push(faces[cardIndex], values[cardIndex]/*,cardCopy*/);
        cardIndex++;
        CalculateProbabilities();
    }

    public void x10()
    {
        apuesta = 10;
        bankValue -= apuesta;
        bank.text = "Tienes " + bankValue + " €";
        x10Button.interactable = false;
        x100Button.interactable = false;
        x1000Button.interactable = false;
        hitButton.interactable = true;
        stickButton.interactable = true;
        apuestaText.text = "";
    }

    public void x100()
    {
        apuesta = 100;
        bankValue -= apuesta;
        bank.text = "Tienes " + bankValue + " €";
        x10Button.interactable = false;
        x100Button.interactable = false;
        x1000Button.interactable = false;
        hitButton.interactable = true;
        stickButton.interactable = true;
        apuestaText.text = "";
    }

    public void x1000()
    {
        apuesta = 1000;
        bankValue -= apuesta;
        bank.text = "Tienes " + bankValue + " €";
        x10Button.interactable = false;
        x100Button.interactable = false;
        x1000Button.interactable = false;
        hitButton.interactable = true;
        stickButton.interactable = true;
        apuestaText.text = "";
    }

    public void Hit()
    {
        /*TODO: 
         * Si estamos en la mano inicial, debemos voltear la primera carta del dealer.
         */

        //Repartimos carta al jugador
        PushPlayer();
        pointsMessage.text = "Tienes " + player.GetComponent<CardHand>().points + " puntos";
        /*TODO:
         * Comprobamos si el jugador ya ha perdido y mostramos mensaje
         */

        if (player.GetComponent<CardHand>().points > 21)
        {
            finalMessage.text = "Te has pasado. HAS PERDIDO";
            hitButton.interactable = false;
            stickButton.interactable = false;
        }
    }

    public void Stand()
    {

        /*TODO:
         * Repartimos cartas al dealer si tiene 16 puntos o menos
         * El dealer se planta al obtener 17 puntos o más
         * Mostramos el mensaje del que ha ganado
         */

        dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);
        hitButton.interactable = false;
        stickButton.interactable = false;

        while (dealer.GetComponent<CardHand>().points < 17)
        {
            PushDealer();
        }
        int dealerPoints = dealer.GetComponent<CardHand>().points;
        int playerPoints = player.GetComponent<CardHand>().points;
        dealerPointsMessage.text = "El dealer ha conseguido "+ dealerPoints + " puntos";

        if (dealerPoints == 21)
        {
            finalMessage.text = "El dealer hizo BlackJack. Has perdido";
        }
        else if (playerPoints == 21)
        {
            finalMessage.text = "Enhorabuena, has hecho BlackJack. Has ganado " + (apuesta * 2).ToString() + " €";
            bankValue += apuesta * 2;
            bank.text = "Tienes " + bankValue + " €";
        }
        else if (dealerPoints > 21)
        {
            finalMessage.text = "La banca se ha pasado, has ganado " + (apuesta * 2).ToString()+" €";
            bankValue += apuesta * 2;
            bank.text = "Tienes " + bankValue + " €";
        }
        else if (dealerPoints == playerPoints)
        {
            finalMessage.text = "Habeis tenido un empate";
            bankValue += apuesta;
            bank.text = "Tienes " + bankValue + " €";
        }
        else if (dealerPoints > playerPoints)
        {
            finalMessage.text = "La banca te ha superado, has perdido";
        }
        else
        {
            finalMessage.text = "Has ganado "+(apuesta*2).ToString()+" € a la banca. Enhorabuena.";
            bankValue += apuesta * 2;
            bank.text = "Tienes " + bankValue + " €";
        }
    }

    public void PlayAgain()
    {
        x10Button.interactable = true;
        x100Button.interactable = true;
        x1000Button.interactable = true;
        finalMessage.text = "";
        dealerPointsMessage.text = "";
        apuestaText.text = "Elije una apuesta:";
        bank.text = "Tienes " + bankValue + " €";
        player.GetComponent<CardHand>().Clear();
        dealer.GetComponent<CardHand>().Clear();
        cardIndex = 0;
        ShuffleCards();
        StartGame();
    }

}
