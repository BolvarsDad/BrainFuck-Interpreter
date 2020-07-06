using System;

namespace Brainfuck
{
    class MainClass
    {

        public static void splash_screen()
        {
            Console.WriteLine("A BrainFuck interpreter by Deceive, 2020");
            Console.WriteLine("Write 'help' for instructions");
        }

        public static void Main(string[] args)
        {
            REPL repl = new REPL();
            splash_screen();
            repl.run();
        }
    }

    public class REPL
    {

        private bool check_line(string line)
        {
            return !line.Equals("end");
        }

        public void clear()
        {
            Console.Clear();
        }

        public void run()
        {
            string uinput;

            while (true)
            {
                Console.Write("\nBrainFuck > ");
                uinput = Console.ReadLine();

                switch (uinput)
                {
                    case ("exit"):
                        Environment.Exit(0);
                        break;

                    case (null):
                        Environment.Exit(0);
                        break;

                    case ("help"):
                        help();
                        break;

                    case ("bf_help"):
                        bf_help();
                        break;

                    case ("run"):
                        buffer();
                        break;

                    case ("clear"):
                        clear();
                        break;

                    default:
                        Console.WriteLine("Unrecognized command");
                        help();
                        break;
                }
            }

        }

        public void buffer()
        {
            string buffer = "";
            string temp;
            Interpreter interpreter;

            Console.WriteLine("Enter your program, then write 'end' on a separate line to execute.");

            while (check_line(temp = Console.ReadLine()))
                buffer += temp;

            interpreter = new Interpreter(buffer);
            interpreter.run();
        }

        public void help()
        {
            Console.WriteLine("exit           exit the program");
            Console.WriteLine("help           prints the help manual");
            Console.WriteLine("bf_help        prints the BrainFuck commands");
            Console.WriteLine("run            runs a brainfuck program");
            Console.WriteLine("clear          clears the console");

        }

        public void bf_help()
        {
            Console.WriteLine("Write command name + '_help' for more detailed explanations of command utility.\n");

            Console.WriteLine("cmd_incr,   '+' - Increases element under pointer.");
            Console.WriteLine("cmd_decr,   '-' - Decreases element under pointer.");
            Console.WriteLine("cmd_left,   '<' - Shifts pointer to the left.");
            Console.WriteLine("cmd_right,  '>' - Shifts pointer to the right.");
            Console.WriteLine("cmd_in,     ',' - Reads char and stores it as an ASCII under the pointer.");
            Console.WriteLine("cmd_out,    '.' - Outputs the ASCII char under the pointer.");
            Console.WriteLine("cmd_loop,   '[' - Starts loop, flag under pointer.");
            Console.WriteLine("cmd_seek,   ']' - Indicates end of loop.");

        }
    }

    public class Interpreter
    {
        private byte[] stack;
        private int ptr;
        private char[] input;

        public Interpreter(string input)
        {
           
            this.input = input.ToCharArray();
            stack = new byte[65535];
        }

        public void run()
        {
            var bracketCounter = 0; //counts encountered brackets
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {

                    case '+':
                        stack[ptr]++;
                        break;

                    case '-':
                        stack[ptr]--;
                        break;

                    case '<':
                        ptr--;
                        break;

                    case '>':
                        ptr++;
                        break;

                    case ',':
                        var key = Console.ReadKey();
                        stack[ptr] = (byte)key.KeyChar;
                        break;

                    case '.':
                        Console.Write(Convert.ToChar(stack[ptr]));
                        break;

                    case '[':
                        if (stack[ptr] == 0)
                        {
                            bracketCounter++;
                            while (input[i] != ']' || bracketCounter != 0)
                            {
                                i++;

                                if (input[i] == '[')
                                {
                                    bracketCounter++;
                                }
                                else if (input[i] == ']')
                                {
                                    bracketCounter--;
                                }
                            }
                        }
                        break;

                    case ']':
                        if (stack[ptr] != 0)
                        {
                            bracketCounter++;
                            while (input[i] != '[' || bracketCounter != 0)
                            {
                                i--;

                                if (input[i] == ']')
                                {
                                    bracketCounter++;
                                }
                                else if (input[i] == '[')
                                {
                                    bracketCounter--;
                                }
                            }
                        }
                        break;
                }
            }
        }

    }

}