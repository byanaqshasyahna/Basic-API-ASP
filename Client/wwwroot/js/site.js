// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Tugas 

/*const animals = [
    { name: "garfield", species: "cat", class: { name: "mamalia" } },
    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "tom", species: "cat", class: { name: "mamalia" } },
    { name: "garry", species: "cat", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "invertebrata" } }
]


let cat = [];
let fish = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        cat.push(animals[i]);
    }

    if (animals[i].species == "fish") {
        animals[i].class.name = "Non-Mamalia"
        fish.push(animals[i])
    }
}

console.log(cat);
console.log(fish);
console.log(animals);*/

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/*TUGAS 2 IMPLEMENTATION MODAL*/

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon?limit=100&offset=1",
    success: function (result) {
        console.log(result.results);
        var text = "";

        $.each(result.results, function (key, val) {
            text += `<tr>
                      <th scope="row">${key+1}</th>
                      <td>${val.name}</td>
                      <td><button class="btn btn-primary" data-toggle="modal" data-target="#modalRelatedContent" onClick="testing('${val.url}')">Details</button></td>
                    </tr>`
        });

        
        $("#tableSW").html(text);

        
    }

})

function testing(url) {
    $.ajax({
        url: url,
        success: function (result) {
            console.log(result.sprites.other['official-artwork'].front_default);

            $('.img-fluid').attr(
                'src',
                `${result.sprites.other['official-artwork'].front_default}`
            )

            var tipePoke = "";
            for (var i = 0; i < result.types.length; i++) {
                var color = "";
                if(result.types[i].type.name == "fire") {
                    color = "badge-danger";
                }
                if (result.types[i].type.name == "grass") {
                    color = "badge-success"
                }
                if (result.types[i].type.name == "poison") {
                    color = "badge-secondary"
                }
                if (result.types[i].type.name == "water") {
                    color = "badge-primary";
                }
                if (result.types[i].type.name == "normal") {
                    color = "badge-info";
                }
                if (result.types[i].type.name == "flying") {
                    color = "badge-warning";
                }
                if (result.types[i].type.name == "bug") {
                    color = "badge-dark";
                }
                tipePoke += `<p class="col text-capitalize bagde ${color} poketypes">${result.types[i].type.name}
                            </p>`
            }

            var pokeAbility = "";
            for (var i = 0; i < result.abilities.length; i++) {
                console.log(result.abilities[i].ability.name);
                pokeAbility += `<li class="row-3">${result.abilities[i].ability.name}
                                </li>`
            }

            
            $(".tipePoke").html( tipePoke);
            $(".pokeAbility").html(pokeAbility);
            $(".height").html(`Height : ${result.height} inch`)
            $(".weight").html(`Weight : ${result.weight} lbs`)
            $(".poke-name").html(result.name);


            $(".hp").css({ "width": `${result.stats[0].base_stat}%` });
            $(".attack").css({ "width": `${result.stats[1].base_stat}%` });
            $(".defense").css({ "width": `${result.stats[2].base_stat}%` });
            $(".speed").css({ "width": `${result.stats[5].base_stat}%` });

            $(".hp_percen").html(`${result.stats[0].base_stat}%`);
            $(".attack_percen").html( `${result.stats[1].base_stat}%` );
            $(".defense_percen").html( `${result.stats[2].base_stat}%` );
            $(".speed_percen").html( `${result.stats[5].base_stat}%` );

            console.log(result);
        }

    })
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $('.table-pokemon').DataTable();
});