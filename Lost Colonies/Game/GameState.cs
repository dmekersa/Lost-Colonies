namespace Lost_Colonies
{
    public class GameState
    {
        public Galaxy theGalaxy { get; private set; }

        public Planet currentPlanet { get; private set; }
        public Planet targetPlanet { get; private set; }

        public GameState()
        {
            theGalaxy = new Galaxy();
            theGalaxy.Generate(256, 256 * 2, 256);

            currentPlanet = theGalaxy.GetFirstPlanet();
            targetPlanet = null;
        }

        public void SetCurrentPlanet(Planet pPlanet)
        {
            currentPlanet = pPlanet;
        }

        public void SetTargetPlanet(Planet pPlanet)
        {
            targetPlanet = pPlanet;
        }
    }
}
