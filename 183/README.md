# EyeCon
EyeCon is our project dedicated to the lovely module 183 here in summer 2021. Crafted with only the finest ingredients - we strive to provide our users only the best of the best. Powered with no less than 69 LinearSVC AI models working diligently for the worlds most excellent user experience.

This is what we at EyeCon stand for. 

## Login
The way registering/logging in a user works is that we build a POST-request with the form-data in JSON-Format to the backend. There the request is processed by either creating a new user or checking if a user with the given credentials exists. 

### Register page
This is how the register screen looks. 
![alt text](https://gitlab.iet-gibb.ch/gbr106767/183-frontend/-/raw/master/images/register.JPG "Img of register screen")

### Login page
And this one is the login page.
![alt text](https://gitlab.iet-gibb.ch/gbr106767/183-frontend/-/raw/master/images/logoin.JPG "Img of login screen")

### API

The login is handled with a call to `/api/v1/authenticate` with the following information:
```
 {
    "username": "<Your username>",
    "password": "<Your password>"
 }   
```
The authentication logic itself is a mix between Spring (Boot), JWT and self written components. To work the way it works there's also some configuration in the background.

```
public String authentication(AuthenticateUserDto dto){
        try {
            //Throws an exception if the username/password-combo is not correct
            authManager.authenticate(
                    new UsernamePasswordAuthenticationToken(dto.getUsername(), dto.getPassword())
            );
            System.out.println("Valid login attempt has been made with the user " + dto.getUsername);
        } catch (BadCredentialsException e) {
            //Log failed attempts
            System.err.println("An invalid login attempt has been made with the username of " + dto.getUsername());
            throw new BadCredentialsException("Incorrect username or password");
        }

        //After successfully authenticating we load the user details from the DB
        final UserDetails userDetails = userService
                .loadUserByUsername(dto.getUsername());

        //We use those details to generate a JWT token
        return jwtUtil.generateToken(userDetails);
    }
```
The generated JWT-Token will then be passed back to the user.

## Session Handling
A secure and reliable way of handling sessions was of utmost importance. Therefore we used [**JSON Web Tokens**](https://jwt.io/) which provides a safe and easy way of authenticating a user as well as the integrity of requests. 

After logging in a such JWT token is created and passed to the user, where it is saved. It is then included in every request which requires authenticatication. That includes the command page.

Because the JWT is saved client side, the server does not maintain any kind of session.


## Command execution
To execute a command one has to visit the command page. There we inserted a multi-selectable dropdown list where the user can select one or multiple command options. A GET-request is then built with the options, where the options are query parameter.

The backend then verifies with a whitelist that only allowed options have been sent. If any illegal options are detected, the request is rejected.
```
    private boolean areArgumentsValid(String[] options) {
        if (options == null) {
            return true;
        }

        for (String option : options) {
            boolean result = Arrays.asList(this.allowedOptions).contains(option);
            if (!result) {
                return false;
            }
        }
        return true;
    }
```

Else if the validity can be confirmed the full command with all specified options is built and executed. The backend then returns the "CommandResultDto" containing a String with the response.

```
public CommandResultDto executeCommand(String[] options) throws CommandException {
        final StringBuilder resultBuilder = new StringBuilder();

        if (!areArgumentsValid(options)) {
            throw new IllegalArgumentException("One of your supplied parameters is not valid");
        }

        final String buildOptions = buildOptions(options);
        try {
            Runtime r = Runtime.getRuntime();
            Process p;
            if (options == null) {
                System.out.println("Executing command 'ip a'");
                p = r.exec("ip a");
            } else {
                System.out.println("Executing command 'ip " + buildOptions + " a'");
                p = r.exec("ip " + buildOptions + " a");
            }
            p.waitFor();
            BufferedReader b = new BufferedReader(new InputStreamReader(p.getInputStream()));
            String line;

            while ((line = b.readLine()) != null) {
                resultBuilder.append(line);
            }

            b.close();

        } catch (IOException | InterruptedException e) {
            throw new CommandException(e.getMessage());
        }

        return new CommandResultDto(resultBuilder.toString());
    }
```


## Logging
The backend logs various events to STDOUT resp. STDERR:
- Valid & Invalid login attempts
    ```
    public String authentication(AuthenticateUserDto dto){
        try {
            authManager.authenticate(
                    new UsernamePasswordAuthenticationToken(dto.getUsername(), dto.getPassword())
            );
            System.out.println("Valid login attempt has been made with the user " + dto.getUsername());
        } catch (BadCredentialsException e) {
            System.err.println("An invalid login attempt has been made with the username of " + dto.getUsername());
            throw new BadCredentialsException("Incorrect username or password");
        }

        final UserDetails userDetails = userService
                .loadUserByUsername(dto.getUsername());

        return jwtUtil.generateToken(userDetails);
    }
    ```
- Valid & Invalid resource calls
    ```
    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain) throws ServletException, IOException {
        ...

        if(authorizationHeader != null && authorizationHeader.startsWith("Bearer ")) {
            jwt = authorizationHeader.substring(7);

            try {
                username = jwtUtil.extractUsername(jwt);
            } catch (SignatureException e) {
                System.err.println("An invalid request to the resource " + request.getRequestURI() + " has been made. Additional Info" +
                        System.lineSeparator() + "Query: " + request.getQueryString() +
                        System.lineSeparator() + "Context Path:" + request.getContextPath());
            }
        }

        if (username != null && SecurityContextHolder.getContext().getAuthentication() == null) {

            UserDetails userDetails = this.userService.loadUserByUsername(username);

            if (jwtUtil.validateToken(jwt, userDetails)) {

                ...

                System.out.println("A valid request has been made to the resource " + request.getMethod() + " " + request.getRequestURI() + " from the UserEntity " + jwtUtil.extractUsername(jwt) + " with possible other info: " +
                        System.lineSeparator() + "Query: " + request.getQueryString() +
                        System.lineSeparator() + "Context Path:" + request.getContextPath());
            }
        }
        filterChain.doFilter(request, response);
    }
    ```


## Persistence
The user credentials are securely saved in a PostgreSQL database. The user table looks like this:

```
@Data
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class UserEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @Column(unique = true, nullable = false)
    private String username;
    @JsonIgnore
    @Column(nullable = false)
    private String password;
}
```

Maybe you already noticed the absence of a separate "Salt"-Attribute? We used bcrypt in the backend which stores the Salt inside the password string. 

### SQL-Injections
We dont have to worry about writing SQL queries ourselves thanks to the used ORM. This also eliminates any possibility of an SQL-Injections

### HTML-Injections
The only thing the user can pass to the DB directly, the password is hashed so that doesn't count, is his username. So that is the only possible attack vector for an HTML-Injection. To prevent that, we escape the name before saving it into the database.
```
    public UserEntity createUser(CreateUserDto dto) {
        String hashedPassword = this.passwordEncoder.encode(dto.getPassword());
        String escapedUsername = HtmlUtils.htmlEscape(dto.getUsername());

        ...

        return repository.save(newUser);
    }
```

## HTTPS and TLS/SSL
The backend aswell as the frontend use SSL/TLS certs from their respective hosting provider. These have been generated automatically, which has saved us some hassle. 

To ensure that the backend really only accepts HTTPS requests we also configured it to do just so.

```
    @Override
    protected void configure(HttpSecurity http) throws Exception {
        ...
        http.requiresChannel().anyRequest().requiresSecure();
    }

```

## Credits
Credit goes to:

- JavaBrains for *Spring Boot + Spring Security + JWT from scratch* - Java Brains" on [YouTube](https://www.youtube.com/watch?v=X80nJ5T7YpE)
- JavaBrains for the very useful JWTUtils class on [GitHub](https://github.com/koushikkothagal/spring-security-jwt/blob/master/src/main/java/io/javabrains/springsecurityjwt/util/JwtUtil.java)
- You for reading the credits :)
