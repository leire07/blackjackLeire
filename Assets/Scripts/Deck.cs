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
    public Text finalMessage;
    public Text probMessage;

     public Text probMessage1;
    public Text probMessage2;

    public int[] values = new int[52];
    int cardIndex = 0;    

    int valuesPlayer = 0;
    int valuesDealer = 0;
    int[] cardsPlayer = new int[50];
    int[] cardsDealer = new int[50];
    int round = 0;
       
    private void Awake()
    {    
        InitCardValues();        

    }

    private void Start()
    {
        ShuffleCards();
        StartGame();        
    }

    private void InitCardValues()
    {
        /*TODO:
         * Asignar un valor a cada una de las 52 cartas del atributo "values".
         * En principio, la posición de cada valor se deberá corresponder con la posición de faces. 
         * Por ejemplo, si en faces[1] hay un 2 de corazones, en values[1] debería haber un 2.
         */

        int aux = 0;

        // i -> palo
        // j -> cartas de un palo

        for (int i = 0; i < 4; i++) //recorre cada palo
        {
            for (int j = 0; j < 13; j++) // recorre cada carta de cada palo
            {
                if (j >= 10) // Si el valor es mayor o igual a 10
                {
                    values[aux] = 10; // siempre valdrá 10
                    aux++;
                }
                else // sino
                {
                    values[aux] = j + 1; // valdrá j + 1
                    aux++;
                }
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
        //Para barajar las cartas

        int randomNumber; 
        Sprite auxFaces;
        int auxValues;

        for (int i = 0; i < 52; i++) // bucle que recorre las 52 cartas
        {
            randomNumber = Random.Range(0, 52); // guardamos un número random que será la casilla
            auxFaces = faces[0]; // auxFaces valdrá lo que valga el primer valor del array faces
            faces[0] = faces[randomNumber]; // la primera casilla cambiará su posición al número aleatorio
            faces[randomNumber] = auxFaces; // el valor guardado en auxFaces lo volvemos a meter
            auxValues = values[0]; 
            values[0] = values[randomNumber]; 
            values[randomNumber] = auxValues;
        }
    }

    void StartGame()
    {

        for (int i = 0; i < 2; i++)
        {
            PushDealer();
            PushPlayer();
            round++;

        }

        /*TODO:
             * Si alguno de los dos obtiene Blackjack, termina el juego y mostramos mensaje
             */

        if (valuesPlayer == 21) // si el valor de las cartas del jugador vale 21
        {
            finalMessage.text = "Blacjack, you win"; // gana el jugador
            stickButton.interactable = false; // inhabilitamos los dos botones
            hitButton.interactable = false;
        }
        else if (valuesDealer == 21) // si el valor de las cartas del dealer vale 21
        {
            finalMessage.text = "Game over, Blacjack"; // pierdes
            stickButton.interactable = false; // inhabilitamos los dos botones
        }
        if (valuesPlayer > 21) // si el valor de las cartas del jugador vale más de 21
        {
            finalMessage.text = "Game over, you've passed"; // pierdes
            stickButton.interactable = false; // inhabilitamos los dos botones
            hitButton.interactable = false;
        }
        else if (valuesDealer > 21) // si el valor de las cartas del dealer vale más de 21
        {
            finalMessage.text = "The dealer has passed, you win"; // ganas
            stickButton.interactable = false; // inhabilitamos los dos botones
            hitButton.interactable = false;
        }
    }

    private void CalculateProbabilities()
    {
        /*TODO:
         * Calcular las probabilidades de:
         * - Teniendo la carta oculta, probabilidad de que el dealer tenga más puntuación que el jugador
         * - Probabilidad de que el jugador obtenga entre un 17 y un 21 si pide una carta
         * - Probabilidad de que el jugador obtenga más de 21 si pide una carta          
         */

        float probabilidad; // variable float de la probabilidad
        int casosPosibles; // variable de casos posibles

        // Teniendo la carta oculta, probabilidad de que el dealer tenga más puntuación que el jugador

        if (round != 0) // si la ronda no es 0
        {
            int visiblesDealer = valuesDealer - cardsDealer[0]; //guardamos las cartas visibles del dealer
            casosPosibles = 13 - valuesPlayer + visiblesDealer; // casos posibles que necesita el dealer para superarte
            probabilidad = casosPosibles / 13f; // dividido entre los casos totales

            if (probabilidad > 1) 
            {
                probabilidad = 1; 
            }
            else if (probabilidad < 0) 
            {
                probabilidad = 0;
            }
            int diferencia = valuesPlayer - visiblesDealer;

            if (diferencia >=10)
            {
                probabilidad = 0;
            }

            probMessage.text = (probabilidad * 100).ToString() + " %"; // el texto de probabilidad será la probabilidad por 100 a string

        }

        //Probabilidad de que el jugador obtenga más de 21 si pide una carta

        float probabilidadMasDe_21; 
        int casosPosiblesMasDe_21;
        casosPosiblesMasDe_21 = 13 - (21 - valuesPlayer);
        probabilidadMasDe_21 = casosPosiblesMasDe_21 / 13f;

        if (probabilidadMasDe_21 > 1) 
        {
            probabilidadMasDe_21 = 1; 
        }
        else if (probabilidadMasDe_21 < 0) 
        {
            probabilidadMasDe_21 = 0; 
        }


        if ((21 - valuesPlayer) > 10)
        {
            probabilidadMasDe_21 = 0;
        }
        probMessage1.text = (probabilidadMasDe_21 * 100).ToString() + " %"; // mostramos la probabilidad


        // Probabilidad de que el jugador obtenga entre un 17 y un 21 si pide una carta

        float probabilidad_17; // variable para la probabilidad de llegar a 17
        int casosPosibles_17; // variable para casos posibles para 17
        casosPosibles_17 = 13 - (16 - valuesPlayer);
        probabilidad_17 = casosPosibles_17 / 13f; // partido los casos totales
        // 16 -> descartar los 16 primeros para coger el 17
        // 13 -> número de cartas de un palo

        if (probabilidad_17 > 1)
        {
            probabilidad_17 = 1; 
        }
        else if (probabilidad_17 < 0) 
        {
            probabilidad_17 = 0; 
        }

        if((16 - valuesPlayer) > 10)
        {
            probabilidad_17 = 0;
        }


        float probabilidad_17_y_21 = probabilidad_17 - probabilidadMasDe_21;

        if (probabilidad_17_y_21 > 1) 
        {
            probabilidad_17_y_21 = 1; 
        }
        else if (probabilidad_17_y_21 < 0) 
        {
            probabilidad_17_y_21 = 0;
        }

        probMessage2.text = (probabilidad_17_y_21 * 100).ToString() + " %"; // mostramos la probabilidad

    }

    void PushDealer()
    {
        dealer.GetComponent<CardHand>().Push(faces[cardIndex], values[cardIndex]);
        valuesDealer += values[cardIndex];
        cardsDealer[round] = values[cardIndex];
        cardIndex++;
    }

    void PushPlayer()
    {
        /*TODO:
         * Dependiendo de cómo se implemente ShuffleCards, es posible que haya que cambiar el índice.
         */

        player.GetComponent<CardHand>().Push(faces[cardIndex], values[cardIndex]);
        valuesPlayer += values[cardIndex];
        cardsPlayer[round] = values[cardIndex];
        cardIndex++;

        CalculateProbabilities();
    }             
    
    public void Hit()
    {
        /*TODO: 
         * Si estamos en la mano inicial, debemos voltear la primera carta del dealer.
         */
        
        //Repartimos carta al jugador
        PushPlayer();

        /*TODO:
         * Comprobamos si el jugador ya ha perdido y mostramos mensaje
         */

        if (valuesPlayer > 21) // si el valor del jugador es mayor que 21
        {
            finalMessage.text = "Game over, you've passed"; // pierdes
            stickButton.interactable = false; // inhabilitamos los botones
            hitButton.interactable = false;

        }

        /*TODO:
       * Comprobamos si el jugador ya ha perdido y mostramos mensaje
       */

        else if (valuesPlayer == 21) // también si el valor del jugador vale 21
        {
            finalMessage.text = "Blacjack, you win"; // ganas
            stickButton.interactable = false; // inhabilitamos los botones
            hitButton.interactable = false;
        }
      

    }

    public void Stand()
    {
        /*TODO: 
         * Si estamos en la mano inicial, debemos voltear la primera carta del dealer.
         */

        hitButton.interactable = false; // inhabilitamos los botones
        dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);

        /*TODO:
       * Repartimos cartas al dealer si tiene 16 puntos o menos
       * El dealer se planta al obtener 17 puntos o más
       * Mostramos el mensaje del que ha ganado
       */

        while (valuesDealer <= 16) // si el dealer tiene 16 puntos o menos
        {
            PushDealer(); // el dealer roba
        }

        if (valuesDealer == 21) // si el valor del dealer es 21
        {
            finalMessage.text = "Game over, Blacjack"; // pierdes
        }
        else if (valuesDealer > 21) // si el valor del dealer es mayor que 21
        {
            finalMessage.text = "The dealer has passed, you win";  // ganas
        }
        else if (valuesDealer < valuesPlayer) // si el valor del dealer es menor que el del jugador
        {
            finalMessage.text = "You win"; // ganas
        } 
        else if (valuesDealer == valuesPlayer) // si el valor del dealer es igual que el del jugador
        {
            finalMessage.text = "Tie (empate)"; // empatas
        }
        else // sino
        {
            finalMessage.text = "Game over"; // pierdes
        }

        stickButton.interactable = false; // inhabilitamos botón               
         
    }

    public void PlayAgain()
    {
        hitButton.interactable = true; // habilitamos los botones
        stickButton.interactable = true;
        finalMessage.text = "";
        player.GetComponent<CardHand>().Clear();
        dealer.GetComponent<CardHand>().Clear();
        cardIndex = 0;
        valuesPlayer = 0;
        round = 0;
        valuesDealer = 0;
        ShuffleCards(); // mezclamos
        StartGame(); // empezamos juego
    }
    
}
