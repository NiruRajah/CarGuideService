<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>Data Extractor</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>

<?php  
    //URL where the json file is
    $url.= "https://vpic.nhtsa.dot.gov/api/vehicles/GetAllManufacturers?format=json&page=2'"
    echo $url;  
    
    //Gets the contents of the Json
    $strJsonFileContents = file_get_contents($url);
    if($strJsonFileContents !== false){
        //Print out the contents.
        echo $contents;
    }
    //Gets the contents of the Json
    //$strJsonFileContents = file_get_contents("css-color-names.json");

    //Converts the json to an array 
    $array = json_decode($strJsonFileContents, true);
    var_dump($array); // print array
    
    echo $_POST["Country"];
    
  ?>
    
</body>
</html>