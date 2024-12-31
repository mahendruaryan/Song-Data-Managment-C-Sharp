#nullable disable
using System.Text.Json;   //json library for saving and loading data 

int LinearSearchTitle(List<Song> alist,string sfind){
    sfind=System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(sfind);  //making the first charcter of each word capital so that user will able to find if he enters in lower case
    for(int indexnum=0;indexnum<alist.Count;indexnum++){
        if(alist[indexnum].Title == sfind){    //checking if the title matches with the given string
            return indexnum;
        }
    }
    return-1;
}      //linearseach for list of songs which will search according to it's title

void BubbleSortTitle(List<Song> anlist){
    for(int b=0;b<anlist.Count-1;b++){//outer loop
        for(int j=0;j<anlist.Count-1-b;j++){//inner loop
        int stringcompare=(string.Compare(anlist[j].Title,anlist[j+1].Title));//comparing the next string to sort 
        if(stringcompare == 1){
            string tit=anlist[j].Title;  //holding value temporarily for title
            string art=anlist[j].Artist;  //holding value temporarily for artist
            string gen=anlist[j].Genre;  //holding value temporarily for genre
            anlist[j].Title=anlist[j+1].Title;
            anlist[j].Artist=anlist[j+1].Artist;
            anlist[j].Genre=anlist[j+1].Genre;
            anlist[j+1].Title=tit;
            anlist[j+1].Artist=art;
            anlist[j+1].Genre=gen;
        }
    }
 
    }
}// bubble sort for list of songs which will sort according to title




List<Song> songs=new List<Song>();
songs.Add(new Song("My Heart Will Go On","Celine Dion","Slow"));
songs.Add(new Song("Open Arms","Journey","Slow Dance"));
songs.Add(new Song("Maybe Im Amazed","Wings","Slow"));
songs.Add(new Song("Unchained Melody","The Righteous Brothers","Tbd"));
songs.Add(new Song("Endless Love","Lionel Richie & Diana Ross","Slow"));
songs.Add(new Song("Your Song","Elton John","Slow"));
songs.Add(new Song("Ill Be There","The Jackson ","Slow"));
songs.Add(new Song("I Dont Want to Miss a Thing","Aerosmith","Rock "));
songs.Add(new Song("At Last","Etta James","Tbd"));
songs.Add(new Song("How Deep Is Your Love","Bee Gees","Easy"));




List<User> Users=new List<User>();
try{
    string jsonString = File.ReadAllText("Users.json");// json to read files
    Users= JsonSerializer.Deserialize<List<User>>(jsonString);// reading files
}
catch{
    //if the file Users is not prsent it will not give error (if Users.json is deleted or not prsent in folder)
}


bool loop=true;
while (loop){
    Console.WriteLine("1. Login");
    Console.WriteLine("2. SignUp");
    Console.WriteLine("3. Exit");
    string selection=Console.ReadLine();    //will store the user inputr for login,signup and exit


   


    int index=0;     // for checking if the user has entered correct username and password or not 
    int index2=0;   // for checking if the username already exist when signing up or if the username field is left blank
    int index3=0;   //for checking that the given genre is found or not when sorting according to genre
    if(selection == "1"){
        Console.WriteLine("Username");
        string uname=Console.ReadLine();//user input for username
        Console.WriteLine("Password");
        string pass=Console.ReadLine();//user input for password
        for(int a=0;a<Users.Count;a++){
            if (Users[a].Username == uname && Users[a].Password == pass){              //checking if username and password matches 
                int indexofuser=a;   // for accessing list faves 
                Console.WriteLine("Succesfully Logined "+ Users[a].Username);
                index=1;  //setting index to 1 so that it changes status to username and password matches and found

                bool loop1=true;
                while (loop1){

                    Console.WriteLine("1.Display all of the data");
                    Console.WriteLine("2.Display some of the data (Sort on the basis of Genre)");
                    Console.WriteLine("3.Sort the data");
                    Console.WriteLine("4.Select data to add to a favourites list ");
                    Console.WriteLine("5.Remove data from favourites list / shopping cart.");
                    Console.WriteLine("6.Display favourites list / shopping cart.");
                    Console.WriteLine("7.Sign Out");
                    string loginselection=Console.ReadLine(); // selection for user after login

                    if(loginselection == "1"){
                        foreach(Song song in songs)
                        Console.WriteLine(song.Title+" By "+song.Artist+" of Genre "+song.Genre);//printing all the data 

                    }

                    else if(loginselection == "2"){
                        Console.WriteLine("Please enter genre of the song to be filter ");
                        string genrefind=Console.ReadLine();  // user will input the name of genre
                        genrefind=System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(genrefind);//user will able to find even if he uses small character
                        foreach(Song song in songs){
                            if (song.Genre == genrefind){
                                Console.WriteLine(song.Title+" By "+song.Artist+" of Genre "+song.Genre);
                                index3=1;//if genre is founded index3 will set to1
                            }
                        }
                        if(index3 == 0){//if no genre with that given name is found
                            Console.WriteLine("You have entered wrong genre");
                        }
                        index3=0;//setting index3 again to zero for next time use
                         
                    }

                    else if(loginselection== "3"){
                        BubbleSortTitle(songs);      //sorting the data
                        Console.WriteLine("Data Sorted on the basis of title");
                    }


                    else if(loginselection == "4"){
                        Console.WriteLine("Please enter a song title to be add in your Favourite List");
                        string selectedsong=Console.ReadLine();        //user input which user want to add to faves list
                        int songindex=LinearSearchTitle(songs,selectedsong);  // index of the song which user wants to add to faves list
                        int linearforchecking=LinearSearchTitle(Users[indexofuser].Faves,selectedsong); //for checking if the song is already present in Favorite List
                        if(songindex == -1){                                  //if user entered song is not in the list
                            Console.WriteLine("You have entered an incorrect song please try again");
                        }
                        else if(linearforchecking>=0){                            //if user has already added the item in faves list
                            Console.WriteLine("The song is already in your favorite list");

                        }

                        else{//adding the item in faves if none of the above situtation is true
                            Users[indexofuser].Faves.Add(new Song(songs[songindex].Title,songs[songindex].Artist,songs[songindex].Genre));
                            Console.WriteLine("Succesfully added");
                        }
                    }

                    else if (loginselection == "5"){
                        Console.WriteLine("Please enter the name of the song you want to remove ");
                        string removingsong=Console.ReadLine();   //user inpt which song user want to remove
                        int linearforremoving=LinearSearchTitle(Users[indexofuser].Faves,removingsong);  //checking if the song is present in faves list or not
                        if(linearforremoving == -1){                                     // if the song is not present in faves list
                            Console.WriteLine("The song which you want to remove is not present in your faves list");
                        }
                        else{
                            Users[indexofuser].Faves.Remove(Users[indexofuser].Faves[linearforremoving]);
                            Console.WriteLine("Succesfully Removed ");
                        }


                    }

                    else if (loginselection == "6"){
                        Console.WriteLine("Favourites list");
                        foreach(Song song in Users[indexofuser].Faves){                   //displaying faves list
                            Console.WriteLine(song.Title+" By "+song.Artist+" of Genre "+song.Genre);
                        }

                    }






                    else if(loginselection == "7"){
                        User.SaveData(Users);// saving the data when user is signing out ,created a static method below
                        loop1=false;
                        Console.WriteLine("Signed Out Successfully");
                        
                    }

                    else{
                        Console.WriteLine("The value should be between 1 to 7");
                    }
                }

            }

        }

        if (index == 0){//giving error if username and password does not matches
            Console.WriteLine("Username and Password does not match");            
        }
    
    
    }



    
    else if(selection == "2"){//signup asking username and password from user 
        Console.WriteLine("Please enter new Username to add");
        string newusername=Console.ReadLine();
        if(newusername == ""){
            index2=1;//user will not able to move forward if he left username blank
            Console.WriteLine("Username cannot be blank");
        }
        else{
            for(int d=0;d<Users.Count;d++){
               if(newusername == Users[d].Username){               //checking if the username is already present in list
                   Console.WriteLine("This User already exist Please try again");
                   index2=1;   // setting index2 to 1 so that user will  will not able to move forward if the same user is already present                         
                }
            }

        }


            

        if(index2 == 0){
            Console.WriteLine("Please enter new Password");
            string newpassowrd=Console.ReadLine();
            if(newpassowrd == ""){// user will not able to move forward if he left password blank
                Console.WriteLine("Password cannot be blank");
            }
            else{
                Users.Add(new User(newusername,newpassowrd));
                Console.WriteLine("New user is succesfully created with username "+newusername);
                User.SaveData(Users);// saving the data when a new user sign ups
            }
            

        }      
    }

    else if(selection=="3"){
        loop=false;
        Console.WriteLine("Bye");
    }

    else{
        Console.WriteLine("The value should be from 1 to 3");
    }

    
    

}


class Song{
    public string Title {get;set;}
    public string Artist{get;set;}
    public string Genre{get;set;}

    public Song(string title,string artist,string genre){
        this.Title=title;
        this.Artist=artist;
        this.Genre=genre;
    }

}

class User {
	public string Username { get; set; }
	public string Password { get; set; }
	public List<Song> Faves { get; set; }
    

	public User(string username,string password) {
		this.Username = username;
		this.Password = password;
		this.Faves = new List<Song>();
	}

    public static void SaveData(List<User> users){//method to save data 
        // convert list of users to JSON string
        string jsonString = JsonSerializer.Serialize(users);
 
        // write JSON string to a file
        File.WriteAllText("Users.json", jsonString);
    }



}
