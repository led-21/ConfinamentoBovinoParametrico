using NetTopologySuite;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Esri;

string shpPath = "C:\\Users\\adria\\OneDrive\\3-Consultoria\\01-2023\\Confinamento Ivinhema\\SHP_Confinamento\\ConfinamentoParametrico.shp";

var features = new List<Feature>();

//Coordenadas SIRGAS 2000 / UTM zone 22S - EPSG:31982
var x = 225427.0143246169;
var y = 7520346.238603081;

//Parâmetros de dimensionamento
int n_baias_na_linha = 12;
int n_de_linhas = 5;

int larguraBaia = 50;
int comprimentoBaia = 50;
int corredorManejo = 10;

//Geração do arquivo shp
for (int i = 0; i < n_de_linhas; i++)
{
    for (int j = 0; j < n_baias_na_linha; j++)
    {
        var coords = new List<CoordinateZ>()
        {
            new CoordinateZ(x, y),
            new CoordinateZ(x+larguraBaia, y),
            new CoordinateZ(x+larguraBaia, y+comprimentoBaia),
            new CoordinateZ(x,y+comprimentoBaia),
            new CoordinateZ(x, y)
        };

        var geoFactory = Geometry.DefaultFactory;
        var poligon = geoFactory.CreatePolygon(coords.ToArray());

        features.Add(new Feature(poligon, null));

        x += larguraBaia;
    }
    x = 225427.0143246169;
    y += comprimentoBaia+corredorManejo;
}

Shapefile.WriteAllFeatures(features, shpPath);