using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using static godot_net_server.OnnxModelScorer;

namespace godot_net_server
{
    class Program
    {
        static void Main(string[] args)
        {


            string projectPath = "C:/Users/Shehroze/source/repos/GymGodot/gym-godot";//  # project.godot folder
            string godotPath = "E:/godot/Godot/Godot_v3.4-stable_mono_win64.exe";//  # godot editor executable
            string scenePath = "./examples/mars_lander/Root.tscn";//  # env Godot scene
            string exeCmd = "cd {} && {} {}'.format(projectPath, godotPath, scenePath)";


            MLContext mlContext = new MLContext();

            // Load trained model

            var modelScorer = new OnnxModelScorer("my_ppo_1_model.onnx", mlContext);
            List<ModelInput> input = new List<ModelInput>();
            input.Add(new ModelInput()
            {
                Features = new[]
                {
                    3.036393f,11.0f,2.958097f,0.0f,0.0f,0.0f,0.015607f,0.684984f,0.0f,0.0f
                }
            });
            var action = modelScorer.Score(mlContext.Data.LoadFromEnumerable(input));
            action.ToString();
            //ITransformer trainedModel = mlContext.Model.Load("my_ppo_1_model.onnx", out modelSchema);
            //trainedModel.ToString();
        }
        
    }
}
