using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using FunicularSwitch;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace Protocol
{
    public class ResultErrorListener<T> : IAntlrErrorListener<T>
    {
        private List<Result<Unit>> Results { get;} = new() { Result.Ok(No.Thing)};

        public Result<Unit> StatusResult
        {
            get
            {
                var errors = Results.Where(r => r.IsError).ToList();
                if (errors.Count == 0)
                    return Result.Ok(No.Thing);
                
                var errorMessages = errors.Select(e => e.Match(_ => "", err => err));
                return Result.Error<Unit>(string.Join("\n", errorMessages));
            }
        }
        
        public void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            Results.Add(Result.Error<Unit>($"{msg}: {offendingSymbol}, {line.ToString()}, {charPositionInLine.ToString()}, ErrorType: {e?.GetType().Name}"));
        }
    }
}