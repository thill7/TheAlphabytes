class FoodDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            details: JSON.parse(this.props.details)
        };
    }

    render() {
        var { details } = this.state;

        return(
            <div className="card bg-primary shadow">
                <div className="card-header">
                    <h2>{details.Description}</h2>
                    <h4 className="font-weight-light">{details.BrandOwner}</h4>
                </div>
                <div className="card-body">
                    <p><span className="font-weight-bold">Ingredients:</span> {details.Ingredients}</p>
                </div>
            </div>
        );
    }
}