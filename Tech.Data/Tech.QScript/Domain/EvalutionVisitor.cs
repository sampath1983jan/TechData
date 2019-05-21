using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.QScript.Domain;
using Tech.QScript.Syntax;

namespace Tech.QScript
{
    public class EvalutionVisitor : IVisitor
    {
        public dynamic result;
                
        public override void Visit(Declare element)
        {
            result = new Result();
            result.Value = element.Name;
        }

        public override void Visit(Fun element)
        {
            if (element.Type == FunctionType.Avg)
            {

            }
            else if (element.Type == FunctionType.Count) {

            }
            else if (element.Type == FunctionType.UCount)
            {

            }
            else if (element.Type == FunctionType.Min)
            {

            }
            else if (element.Type == FunctionType.Max)
            {

            }
            else if (element.Type == FunctionType.Sum)
            {

            }
            else if (element.Type == FunctionType.Pivot)
            {

            }
            else if (element.Type == FunctionType.Mean)
            {

            }
            else if (element.Type == FunctionType.Mode)
            {

            }
            else if (element.Type == FunctionType.Range)
            {

            }
            else if (element.Type == FunctionType.Medium)
            {

            }
            else if (element.Type == FunctionType.Merge)
            {

            }
            else if (element.Type == FunctionType.Join)
            {

            }
            else if (element.Type == FunctionType.Filter)
            {

            }
            else if (element.Type == FunctionType.GetValue)
            {

            }
            else if (element.Type == FunctionType.GetValues)
            {

            }
            else if (element.Type == FunctionType.toString)
            {

            }
        }

        public override void Visit(IterativeStatement element)
        {
            throw new NotImplementedException();
        }

        public override void Visit(SelectionStatement element)
        {
            throw new NotImplementedException();
        }

        public override void Visit(Syntax.Expression element)
        {            
            var a = new NCalc.Expression(element.formula).Evaluate();
            result = new Result();
            result.Value = a;            
        }

        public override void Visit(SQuery element)
        {
            throw new NotImplementedException();
        }

        public override void Visit(Syntax.Value element)
        {
            throw new NotImplementedException();
        }

        #region Functions  & Methods
        private void filter(Fun element) {

        }
        #endregion

    }
}
