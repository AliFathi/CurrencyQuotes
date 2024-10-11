### 1.1- How long did you spend on the coding assignment?
I spend about 6 hours. playing around third-party APIs take the most time.

### 1.2- What would you add to your solution if you had more time?
I'd like to implement a Caching mechanism to store all Currency Symbols.
e.g. use InMemoryCache of .net core.

also I would like to use **Vue.js** as frontend framework,
but I preferred to use plain javascript for such a simple UI.

---

### 2- What was the most useful feature that was added to the latest version of your language of choice?
I love C#. recent versions support Primary Constructors which i liake a lot!

    public class CurrencyController(ILogger<CurrencyController> logger) : ControllerBase
    {
        ...
    }

also i really like "Raw String literals" which allows to write multi-line strings easily and beautifully:
	
    var query = """
        Select *
        From [Table]
        Where [CompanyId] = 1
        Order BY [Id] DESC;
    """;

there are some other simplifications of syntax, like "Top-Level-Statements", or namespaces without braces. I've used all of them in recent projects.

---

### 3- How would you track down a performance issue in production? Have you ever had to do this?
I'm a LOGGER :smile: I love to log everything especially HTTP requests, so I can measure response times.

I use some tools like `Datalust/Seq` to monitor events and create dashbords.

I also monitor CPU/RAM usage of server or database by internal tools like `htop` or `atop`.

---

### 4- What was the latest technical book you have read or tech conference you have been to? What did you learn?
Last month I participated in an AI event held by Sharif University. I learned basic concepts of Image-Generation like **Noising/Denoising**.
also i learned how to run **Llama3** model on a Google CoLab server.

---

### 5- What do you think about this technical assessment?
It was GOOD!

---

### 6- Please, describe yourself using JSON.

    {
      "age": 32,
      "species": "homo sapiens",
      "common name": "ali fathi",
      "scientific name": "alif",
      "location": "tehran",
      "habitat": [ "bed", "work" ],
      "diet": [ "tea", "cookie", "pastry" ],
      "lifestyle": "coding",
      "color": "olive",
      "weight": "65kg",
      "height": "180cm",
      "most distinctive features": [ "logical", "loud" ]
    }
