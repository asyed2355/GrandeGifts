// Initialize and add the map
var geocoder;
var map;
function initMap() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(-33.889348, 151.1583713);
    var mapOptions = {
        zoom: 12,
        center: latlng
    };
    map = new google.maps.Map(document.getElementById('map'), mapOptions);
}

// Function written by me to format address:
function formatAddress() {
    var streetAddress = document.getElementById("streetAddress").value;
    var suburb = document.getElementById("suburb").value;
    var state = document.getElementById("state").value;
    return streetAddress + ", " + suburb + ", " + state + ", Australia";
}

function DeleteMarkers() {
    //Loop through all the markers and remove
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

function clearMarkers() {
    setMapOnAll(null);
}

function codeAddress() {
    var address = formatAddress();
    map.clearMarkers();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}