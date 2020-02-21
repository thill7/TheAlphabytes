class FoodDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            details: JSON.parse(this.props.details)
        };
    }

    render() {
        var { details } = this.state;

        return (
            <div className="pt-4">
                <div className="card bg-secondary text-white shadow">
                    <div className="card-header">
                        <h2 className="display-4 font-weight-normal">{details.Description}</h2>
                        <h3 className="font-weight-light">{details.BrandOwner}</h3>
                    </div>
                    <div className="card-body">
                        <p className="text-lowercase"><span className="font-weight-bold text-capitalize">Ingredients:</span> {details.Ingredients}</p>
                        <p><span className="font-weight-bold">UPC:</span> {details.UPC}</p>
                        <p><span className="font-weight-bold">Serving Size:</span> {details.ServingSize}{details.ServingSizeUnit}</p>
                    </div>
                </div>
            </div>
        );
    }
}