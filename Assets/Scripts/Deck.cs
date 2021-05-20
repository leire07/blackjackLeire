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

    public int[] values = new int[52];
    int cardIndex = 0;    
       
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

        int varAux = 0; 

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                values[varAux] = j + 1;
                varAux++;
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
        

        int rndNum; // variable para el num random
        Sprite tempFaces;
        int tempValues;

        for (int i = 0; i < 52; i++) // bucle que recorre las 52 cartas
        {
            rndNum = Random.Range(0, 52); // num random
            tempFaces = faces[0]; // tempFaces valdrá lo q valga el primer valor del array faces
            faces[0] = faces[rndNum]; // el valor valdrá un rndnum
            faces[rndNum] = tempFaces; // ese eleemento valdrá tempFaces
            tempValues = values[0]; // tempvalues valdrá el valor de values primera cas
            values[0] = values[rndNum]; // que será un numRandom
            values[rndNum] = tempValues; // y ese valdrá tempValues
        }
    }

    void StartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            PushPlayer();
            PushDealer();
            /*TODO:
             * Si alguno de los dos obtiene Blackjack, termina el juego y mostramos mensaje
             */
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
    }

    void PushDealer()
    {
        /*TODO:
         * Dependiendo de cómo se implemente ShuffleCards, es posible que haya que cambiar el índice.
         */
        dealer.GetComponent<CardHand>().Push(faces[cardIndex],values[cardIndex]);
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

        else if (valuesPlayer == 21) // también si el valor del jugador vale 21
        {
            finalMessage.text = "Blacjack, you win!"; // ganas
            stickButton.interactable = false; // inhabilitamos los botones
            hitButton.interactable = false;
        }

    }

    public void Stand()
    {
        /*TODO: 
         * Si estamos en la mano inicial, debemos voltear la primera carta del dealer.
         */

        /*TODO:
         * Repartimos cartas al dealer si tiene 16 puntos o menos
         * El dealer se planta al obtener 17 puntos o más
         * Mostramos el mensaje del que ha ganado
         */                
         
    }

    public void PlayAgain()
    {
        hitButton.interactable = true;
        stickButton.interactable = true;
        finalMessage.text = "";
        player.GetComponent<CardHand>().Clear();
        dealer.GetComponent<CardHand>().Clear();          
        cardIndex = 0;
        ShuffleCards();
        StartGame();
    }
    
}
