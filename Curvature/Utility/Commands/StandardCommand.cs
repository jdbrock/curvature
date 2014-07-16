using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Curvature
{
    public class StandardCommand<T> : ICommand
    {
        // ===========================================================================
        // = Public Events
        // ===========================================================================

        public event EventHandler CanExecuteChanged;

        // ===========================================================================
        // = Private Fields
        // ===========================================================================

        private Action<T> _action;

        // ===========================================================================
        // = Construction
        // ===========================================================================

        public StandardCommand(Action<T> inAction)
        {
            _action = inAction;
        }

        // ===========================================================================
        // = Public Methods
        // ===========================================================================
        
        public Boolean CanExecute(Object inParameter) { return true; }

        public void Execute(Object inParameter)
        {
            _action((T)inParameter);
        }
    }

    public class StandardCommand : StandardCommand<Object>
    {
        public StandardCommand(Action<Object> inAction) : base(inAction) { }
    }
}
