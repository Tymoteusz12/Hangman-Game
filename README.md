# Hangman-Game

#### H4Message for Motorola:
#### H4At the end of this Readme I added few extra words about my motivations for programming. This repository is going to be public until 12.11.

# H1Very simple Hangman Game. 

After game starts, program will draw random capital city from given txt file. Your task is to guess the city! 
You have 5 lives. At the beginning, every letter of drawn city will be displayed as single underscore.
You can either enter whole city - but if you miss, you will lose 2 lives, or enter single letter. Miss of single letter will result in lose of 1 live. 
If you hit - underscore/s will be replaced with the corresponding letter/s of the city. 

If you win - your score will be saved in the table of glory! You can save your score in High scores table file and display it before game starts or after you win/lose.

#### H4Below you can see example gameplay:

```cmd
        Welcome to Hangman game! Would you like to proceed?
        Enter 1 to start the game, enter 2 to show highscores or 0 to exit application.
>>>>>>> 1

        The topic of game is: Country capitals on Earth.

        Let's play!

        You still have 5 lives left.

                 + ------ +
                 |        |
                          | <=== This is gibbet!
                          |
                          |
                          |
                ==========

        Current result is:

        _ _ _ _ _ _ _ _ _

        Your (used && missed) letters:  []
        Your (used && missed) cities:   []

        Would you like to enter single letter or whole city name?
        Enter 1 for single letter or
        Enter 0 for whole city name:
>>>>>>> 0
Please enter your city guess:
>>>>>>> Ihavenoidea
        The topic of game is: Country capitals on Earth.


        Looks like length of your input is invalid and doesn't match with hidden city. As a result you lost two lives!


        You still have 3 lives left.

                 + ------ +
                 |        |
                 O        |
                 |\       |
                          |
                          |
                ==========

        Current result is:

        _ _ _ _ _ _ _ _ _

        Your (used && missed) letters:  []
        Your (used && missed) cities:   []

        Would you like to enter single letter or whole city name?
        Enter 1 for single letter or
        Enter 0 for whole city name:
>>>>>>> 0
Please enter your city guess:
>>>>>>> x
        The topic of game is: Country capitals on Earth.


        Looks like length of your input is invalid and doesn't match with hidden city. As a result you lost two lives!


        You still have 1 life left.

                 + ------ +
                 |        |
                 O        |
                /|\       |
                 |        |
                /         |
                ==========

        Current result is:

        _ _ _ _ _ _ _ _ _

        Your (used && missed) letters:  []
        Your (used && missed) cities:   []

        I would like to give you a hint. This city is a capital of Montenegro

        Would you like to enter single letter or whole city name?
        Enter 1 for single letter or
        Enter 0 for whole city name:
>>>>>>> 0
Please enter your city guess:
>>>>>>> Podgorica

        You won! The city was capital of: Montenegro, Podgorica.

        You managed to guess city in : 198 s
        You guessed after 0 letters!
        You scored: 516 score and saved Hangman!


        ------------------------------------------ High score ---------------------------------------------------
        | Name          |       Date            |Elapsed time   |Guessed city                   |       Score   |

        | Tymoteusz...  | 05.11.2020 12:57:30   |        18     | Tehran                        |       588     |
        | Tymoteusz...  | 05.11.2020 12:52:16   |        14     | Cairo                         |       500     |
        | Tymoteus...   | 05.11.2020 12:51:09   |        23     | Kuala lumpur                  |       1154    |

        Would you like to save your score? Enter 1 for yes, enter 0 for no:
>>>>>>> 1
        Enter your name:
>>>>>>> Tymoteusz
        I saved your score to the table of glory!
        
        The game has ended. Would you like to play again?
        Enter 1 to play again or enter 0 to exit game.
>>>>>>> 0
        Thank you for playing!

When I started programming my main motivation was curiosity. I felt like it is possible for me and I would like to be good at something. I knew that I have predispositions for science and I planned to start programming. I had friends who were into coding and they inspired me. Also I liked new technologies and all this stuff. 
I love to solve mental tasks, even though they are sometimes tough. Honestly - the harder task seems for my mind - the more I like it.
I am also passionated about physics and during my programming journey I used it few times for creating very simple 2D/3D simulations. 
I think writting code is just something I like. I want to solve tasks, develop my self and keep my brain as sharpest as possible.
I choosed backend due to my small interest in cybersecurity. Also I tried some Front-end, but it was a little bit to high level - I prefer coding on lower levels and closer to the machines\servers.  
I must not hide, that Motorola Academy started to interest me. 600 hours of programming knowledge from profesionals! Big chance to learn a lot of stuff and start very interesting journey. I am highly motivated to face this up - no doubts!
