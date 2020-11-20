import requests
import json

#Get
url = 'https://vpic.nhtsa.dot.gov/api/vehicles/GetAllManufacturers?format=json&page=2';
request = requests.get(url);
#print(request.text);

data = json.loads(request.text)
#print(data['Results'])

print("All manufacturers from Japan: ")
#Query to get all manufacturers from Japan.
for x in data['Results']:
    if x['Country'] == 'JAPAN':
        print("Manufacturer's name: " + x['Mfr_Name'])
        print("Manufacturer's ID: " + str(x['Mfr_ID']))
        print()

#Post
#url = 'https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVINValuesBatch/';
#post_fields = {'format': 'json', 'data':'3GNDA13D76S000000;5XYKT3A12CG000000'};
#r = requests.post(url, data=post_fields);
#print(r.text);