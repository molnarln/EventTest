using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EventTest
{
    class Program
    {
        static void MaximumReached(object source, MyEventArgs e)
        {
            Console.WriteLine(e.GetInfo());
        }

        static void Main(string[] args)
        {
            // Now lets test the event contained in the above class.
            MyClass MyObject = new MyClass();
            MyObject.OnMaximum += new EventHandler<MyEventArgs>(MaximumReached);
            for (int x = 0; x <= 15; x++)
            {
                MyObject.MyValue = x;
            }
            Console.ReadLine();
        }
    }

    // First we have to define a delegate that acts as a signature for the
    // function that is ultimately called when the event is triggered.
    // You will notice that the second parameter is of MyEventArgs type.
    // This object will contain information about the triggered event.

    public delegate void MyEventHandler(object source, MyEventArgs e);

    // This is a class which describes the event to the class that receives it.
    // An EventArgs class must always derive from System.EventArgs.

    public class MyEventArgs : EventArgs
    {
        private string EventInfo;

        public MyEventArgs(string Text)
        {
            EventInfo = Text;
        }

        public string GetInfo()
        {
            return EventInfo;
        }
    }

    // This next class is the one which contains an event and triggers it
    // once an action is performed. For example, lets trigger this event
    // once a variable is incremented over a particular value. Notice the
    // event uses the MyEventHandler delegate to create a signature
    // for the called function.

    public class MyClass
    {
        public event EventHandler<MyEventArgs> OnMaximum;

        private int i;
        private int Maximum = 10;

        public int MyValue
        {
            get { return i; }
            set
            {
                if (value <= Maximum)
                {
                    i = value;
                }
                else
                {
                    // To make sure we only trigger the event if a handler is present
                    // we check the event to make sure it's not null.
                    if (OnMaximum != null)
                    {
                        OnMaximum(this, new MyEventArgs("The value is: " + value.ToString()
                            + " but the maximum is: "
                            + Maximum
                            ));
                    }
                }
            }
        }
    }
}
