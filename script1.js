$.ajax({
    url:'http://localhost:4000/users/1'
    .then(response => {
        console.log(response);
    }).catch(error => {
        console.log(error);
    })
})