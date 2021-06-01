using MarsRovers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Controller
{
    class RoversSet
    {
        //If the Set is configured
        public bool IsConfigured { get; set; } = false;

        private List<Rover> _rovers = new List<Rover>();
        private int[] _boundary;

        public RoversSet() { }

        public RoversSet(int[] boundaries)
        {
            _boundary = new int[boundaries.Length];
            boundaries.CopyTo(_boundary, 0);
            IsConfigured = true;
        }
        /// <summary>
        /// Get the size of the set's map.
        /// </summary>
        /// <returns></returns>
        public string GetBoundaries()
        {
            if (IsConfigured)
                return $"[{_boundary[0]} {_boundary[1]}]";
            else
                return "NO CONFIGURED";
        }
        /// <summary>
        /// Adds a new rover to the set.
        /// </summary>
        /// <param name="num">Number of the rover</param>
        /// <param name="coords">Initial position of the rover [Number Number Character]</param>
        /// <param name="instructions">String of actions [L|R|M]</param>
        public void AddNewRover(int num, string[] coords, string instructions)
        {
            Rover newRover = new Rover();
            newRover.Number = num;
            newRover.PosX = int.Parse(coords[0]);
            newRover.PosY = int.Parse(coords[1]);
            newRover.Facing = coords[2][0];
            newRover.CurrentInstructions = instructions;
            _rovers.Add(newRover);
        }
        /// <summary>
        /// Executes the instructions for each one of the rovers, one after another, and returns theirs previous and current data. Also checks for boundary transgression.
        /// </summary>
        /// <returns>A list of previous and current data</returns>
        public List<string> Run()
        {
            if (!IsConfigured)
                return new List<string>() { "NO CONFIGURED" };
            //Previous status
            List<string> outputs = _rovers.Select(r => $"Rover # {r.Number}: Previous location = [{r.PosX} {r.PosY} {r.Facing}] Instructions = [{r.CurrentInstructions}]").ToList();
            List<string> notes = new List<string>();
            foreach (Rover rover in _rovers)
            {
                string tracker = "";
                //Process instructions
                foreach (char x in rover.CurrentInstructions)
                {
                    string dirInst = $"{rover.Facing}{x}";
                    tracker += $"{x}";
                    //N|S|E|W| + |L|R|M
                    switch (dirInst)
                    {
                        case "SR":
                        case "NL": rover.Facing = 'W'; break;
                        case "NR":
                        case "SL": rover.Facing = 'E'; break;
                        case "WR":
                        case "EL": rover.Facing = 'N'; break;
                        case "ER":
                        case "WL": rover.Facing = 'S'; break;
                        case "NM": rover.PosY += 1; break;
                        case "SM": rover.PosY -= 1; break;
                        case "EM": rover.PosX += 1; break;
                        case "WM": rover.PosX -= 1; break;
                    }
                    if (rover.PosX < 0 || rover.PosY < 0 || rover.PosX > _boundary[0] || rover.PosY > _boundary[1])
                    {
                        //Boundary transgression
                        if (!notes.Where(o => o.StartsWith($"Note: Rover # {rover.Number} has gone out of grid limits")).Any())
                        {
                            //Notify only the first time for each rover
                            notes.Add($"Note: Rover # {rover.Number} has gone out of grid limits. (Trace: {tracker})");
                        }
                    }
                }
            }
            //Current status
            outputs.Add("Output:");
            outputs.AddRange(_rovers.Select(r => $"Rover # {r.Number}: Current location = [{r.PosX} {r.PosY} {r.Facing}]").ToList());
            if (notes.Count > 0)
            {
                outputs.Add("Notes:");
                outputs.AddRange(notes);
            }
            return outputs;
        }
    }
}