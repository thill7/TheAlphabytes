# **Coding Guidelines**

All code will be self documenting and will not contain any comments. If a piece of code requires additional explanation, a supplementary markdown file will be drafted by the developer.

### HTML
- All code shall use Bootstrap 4's grid system.
- All Id's will be named with Pascal case without abbreviations.
    - Example. 
        ```html
        <div id="MySpecialDiv">
            I am a special little div!
        <\div>
        ```
- Wherever possible and appropriate, use Razor Pages to generate HTML.

### CSS
- Bootstrap 4 will be prioritized over custom CSS unless required.
- All custom CSS classes will be named with Pascal case without abbreviations. The opening curly bracket should be on the same line as the class name.
    - Example.
        ```css
        .MySpecialClass {
            width: 100%;
        }
        ```
- All custom CSS will reside in the `site.css` file. There will be no inline CSS.

### C#
- All C# code will follow Microsoft's conventions found here: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions. 

### SQL
- All table names will be singular (except for the auto generated Identity tables).
- All primary keys will be named with the table name followed by `ID`.
    - Example.
        ```sql
        CREATE TABLE [dbo].[Person]
        (
            [PersonID]  INT IDENTITY (1,1)  NOT NULL,
            CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED ([PersonID] ASC)
        )
        ```

### Javascript
- All javascript code will be written using the React Javascript library.

### GIT
- All branches will be named using snake case starting with the initials of the developer creating the branch.
    - Example
    ```git
    git checkout -b gb_my_special_branch
    ```
- Additionally, a label with the branch name will be added to the user story in Pivotal Tracker associated with the branch.
- Commit often, even if the item being worked on has not been completed yet.
- For commit messages, use full, meaningful sentences in past tense.