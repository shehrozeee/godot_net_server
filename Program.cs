using System;
using System.Linq;
using Microsoft.ML;

namespace godot_net_server
{
    class Program
    {
        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // Load trained model
            var modelScorer = new OnnxModelScorer("my_ppo_1_model.onnx", mlContext);
            var input = new[]
            {
                3.036393f,11.0f,2.958097f,0.0f,0.0f,0.0f,0.015607f,0.684984f,0.0f,0.0f
            };
            var action = modelScorer.Score(input);
            var action_command = action.ToList().IndexOf(action.ToList().Max());
            Console.Write(action_command);
        }
        
    }
}
