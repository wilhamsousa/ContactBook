const faker = require('faker-br');

let userList = [];
for (let index = 0; index < 1000; index++) {
    const newUser =
    {
        "name": faker.name.firstName() + " " + faker.name.lastName(),
        "email": faker.internet.email().toLowerCase(),
        "cpf": faker.br.cpf(),
        //"phoneNumber": "+55" + faker.phone.phoneNumber().replace(/\s+/g, ""),
        "phoneNumber": "+55(41)9999-9999",
        "address": faker.address.streetAddress(),
        "cep": "80000-000",
        "city": faker.address.city(),
        "uf": "PR",
        "complement": "Perto da rua X",
        "geographicalPosition": faker.address.latitude() + " " + faker.address.longitude()
    };
    console.log(newUser);
    userList.push(newUser);    
}

const jsonString = JSON.stringify(userList, null, 2); // Pretty-print with 2-space indentation

const fs = require('fs');

// Save the JSON string into a file
fs.writeFile('list 1mil.json', jsonString, (err) => {
  if (err) {
    console.error('Error writing file:', err);
  } else {
    console.log('File successfully written as list.json');
  }
});